﻿//-------------------------------------------------------------------------------------------------
// <copyright file="UploadService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Interfaces;
    using RentItService.Library;

    using Tools;

    /// <summary>
    /// The RentIt service class.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    public partial class Service : IUploadService
    {
        /// <summary>
        /// Used to upload a file to the database.
        /// </summary>
        /// <param name="token">The user token.</param>
        /// <param name="uploadRequest">The RemoteFileStream to upload.</param>
        /// <param name="movieObject">The movie object.</param>
        /// <returns>True if upload was successful, false if not.</returns>
        public bool UploadFile(string token, RemoteFileStream uploadRequest, Movie movieObject)
        {
            Contract.Requires(movieObject != null && movieObject.FilePath != null && uploadRequest != null);
            User user = User.GetByToken(token);
            if (user.Type != UserType.ContentProvider)
            {
                throw new Exception(); // TODO: Throw better exception.
            }

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
    }
}