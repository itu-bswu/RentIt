//-------------------------------------------------------------------------------------------------
// <copyright file="UploadService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using Entities;
    using Interfaces;
    using Library;
    using Tools;

    /// <summary>
    /// The RentIt service class.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    public partial class Service : IUploadService
    {
        /// <summary>
        /// Upload a new media file, and add a new movie with that file.
        /// </summary>
        /// <param name="token">The user token.</param>
        /// <param name="uploadRequest">The RemoteFileStream to upload.</param>
        /// <param name="movieObject">The movie object with the movie information.</param>
        /// <author>Jakob Melnyk</author>
        public void UploadFile(string token, RemoteFileStream uploadRequest, Movie movieObject)
        {
            if (movieObject != null && movieObject.FilePath != null && uploadRequest != null)
            {
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
                            FilePath = temporaryFilePath
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
                        Stream sourceStream = uploadRequest.FileByteStream;
                        using (targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            const int BufferLength = 8192;
                            byte[] buffer = new byte[BufferLength];
                            int count;
                            while ((count = sourceStream.Read(buffer, 0, BufferLength)) > 0)
                            {
                                targetStream.Write(buffer, 0, count);
                            }

                            targetStream.Close();
                            sourceStream.Close();
                        }
                    }
                    catch (Exception e)
                    { // In case filestream fails, movie has to be deleted from database.

                        var movie = db.Movies.First(m => m.FilePath == temporaryFilePath);
                        db.Movies.Remove(movie);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                throw new Exception(); // TODO: Throw some exception pertaining to null values.
            }
        }
    }
}