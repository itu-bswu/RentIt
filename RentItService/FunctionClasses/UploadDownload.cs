// -----------------------------------------------------------------------
// <copyright file="UploadDownload.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItService.FunctionClasses
{
    using System;
    using System.Diagnostics.Contracts;
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
        /// <author>Jakob Melnyk</author>
        public static bool UploadMovieFile(string token, RemoteFileStream uploadRequest, Movie movieObject)
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

            Movie.RegisterMovie(token, movieObject);
            bool state = UploadFile(token, uploadRequest, movieObject.ID);
            if (state == false)
            {
                Movie.DeleteMovie(token, movieObject);
            }

            return state;
        }

        /// <summary>
        /// Uploads a file to the database.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="uploadRequest">The upload request.</param>
        /// <param name="movieID">ID of the media this file belongs to.</param>
        /// <returns>True if the upload succeeded, false if it failed. </returns>
        /// <author>Jakob Melnyk</author>
        public static bool UploadFile(string token, RemoteFileStream uploadRequest, int movieID)
        {
            Contract.Requires<ArgumentNullException>(uploadRequest != null);
            Contract.Requires<ArgumentNullException>(uploadRequest.FileByteStream != null &
                                                      uploadRequest.FileName != null);

            Contract.Requires<InsufficientRightsException>(User.GetByToken(token).Type == UserType.ContentProvider);

            using (var db = new RentItContext())
            {
                var tempMovie = db.Movies.Find(movieID);
                if (tempMovie == null)
                {
                    throw new NoMovieFoundException("Movie was not found in the database.");
                }

                if (tempMovie.OwnerID != db.Movies.Find(movieID).OwnerID)
                {
                    throw new InsufficientRightsException("This user was not the one who registered the movie.");
                }

                var movieFilePath = tempMovie.ID + "_" + tempMovie.Title + Path.GetExtension(uploadRequest.FileName);

                try
                {
                    var filePath = Path.Combine(Constants.UploadDownloadFileFolder, movieFilePath);
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
                }
                catch
                {
                    return false;
                }

                tempMovie.FilePath = movieFilePath;
                db.SaveChanges();
                return true;
            }
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
            Contract.Requires<ArgumentException>(downloadRequest.ID > 0);

            using (var db = new RentItContext())
            {
                var user = User.GetByToken(token);
                if (!(user.Rentals.Any(x => x.MovieID == downloadRequest.ID & x.UserID == user.ID)))
                {
                    throw new InsufficientRightsException();
                }

                var movie = db.Movies.First(m => m.ID == downloadRequest.ID);

                var filePath = Path.Combine(Constants.UploadDownloadFileFolder, movie.FilePath);
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