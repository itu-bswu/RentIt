// -----------------------------------------------------------------------
// <copyright file="UploadDownload.cs" company="RentIt">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.FunctionClasses
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using Entities;
    using Enums;
    using Exceptions;
    using Library;

    using Tools;

    /// <summary>
    /// Functionality for upload and download.
    /// </summary>
    public class UploadDownload
    {
        /// <summary>
        /// Upload a new media file, and add a new movie with that file.
        /// </summary>
        /// <param name="token">The user token.</param>
        /// <param name="uploadRequest">The RemoteFileStream to upload.</param>
        /// <param name="movieObject">The movie object with the movie information.</param>
        /// <returns>True if upload was successful, false if not.</returns>
        public static bool UploadFile(string token, RemoteFileStream uploadRequest, Movie movieObject)
        {
            Contract.Requires<ArgumentNullException>(token != null);

            Contract.Requires<ArgumentNullException>(uploadRequest != null);
            Contract.Requires<ArgumentNullException>(uploadRequest.FileByteStream != null &
                                                      uploadRequest.FileName != null);

            Contract.Requires<ArgumentNullException>(movieObject != null);
            Contract.Requires<ArgumentNullException>(movieObject.Description != null &
                                                      movieObject.Genre != null &
                                                      movieObject.Title != null);

            Contract.Requires<InsufficientRightsException>(User.GetByToken(token).Type == UserType.ContentProvider);

            // TODO: Figure out safer way to determine temporary filepath.
            string temporaryFilePath = DateTime.Now.ToString(CultureInfo.InvariantCulture) + movieObject.Title;

            using (var db = new RentItContext())
            {
                // Creates the new movie in the database.
                var newMovie = new Movie
                {
                    Description = movieObject.Description,
                    Genre = movieObject.Genre,
                    Title = movieObject.Title,
                    FilePath = temporaryFilePath,
                    Owner = User.GetByToken(token)
                };
                db.Movies.Add(newMovie);
                db.SaveChanges();

                // Sets the new filepath.
                var tempMovie = db.Movies.First(m => m.FilePath == temporaryFilePath);
                tempMovie.FilePath = temporaryFilePath + "_" + Path.GetExtension(uploadRequest.FileName);
                db.SaveChanges();

                // Attempts to upload the file to the server.
                try
                {
                    string filePath = Path.Combine(Constants.UploadDownloadFileFolder, tempMovie.FilePath);

                    FileStream targetStream;
                    var sourceStream = uploadRequest.FileByteStream;
                    using (targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        const int BufferLength = 8192;
                        var buffer = new byte[BufferLength];
                        int count;
                        while ((count = sourceStream.Read(buffer, 0, BufferLength)) > 0)
                        {
                            targetStream.Write(buffer, 0, count);
                        }

                        targetStream.Close();
                        sourceStream.Close();
                    }

                    return true;
                }
                catch
                { // In case filestream fails, movie has to be deleted from database.

                    var movie = db.Movies.First(m => m.FilePath == temporaryFilePath);
                    db.Movies.Remove(movie);
                    db.SaveChanges();
                }
            }

            return false;
        }

        /// <summary>
        /// Creates a stream for downloading a file from the server. 
        /// The movie is identified by the ID in the instance of the Movie class.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="downloadRequest">The movie to download.</param>
        /// <returns>The stream information necessary for download.</returns>
        /// <author>Jakob Melnyk</author>
        public static RemoteFileStream DownloadFile(string token, Movie downloadRequest)
        {
            Contract.Requires<ArgumentNullException>(token != null);

            Contract.Requires<ArgumentNullException>(downloadRequest != null);
            Contract.Requires(downloadRequest.Genre != null &
                              downloadRequest.Description != null &
                              downloadRequest.Title != null);

            using (var db = new RentItContext())
            {
                User user = User.GetByToken(token);
                if (!(user.Rentals.Any(x => x.MovieID == downloadRequest.ID & x.UserID == user.ID)))
                {
                    throw new InsufficientRightsException();
                }

                string filePath;

                var movie = db.Movies.First(m => m.ID == downloadRequest.ID);
                filePath = Path.Combine(Constants.UploadDownloadFileFolder, movie.FilePath);

                var fileInfo = new FileInfo(filePath);

                // Check to see if file exists.
                if (!fileInfo.Exists)
                {
                    throw new FileNotFoundException("File not found", fileInfo.Name);
                }

                // Open stream
                var stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);

                // Set up rfs
                return new RemoteFileStream(movie.FilePath, fileInfo.Length, stream);
            }
        }
    }
}
