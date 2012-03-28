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

    using RentItService.Entities;
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
        /// <author>Jakob Melnyk</author>
        public void UploadFile(string token, RemoteFileStream uploadRequest, Movie movieObject)
        {
            if (movieObject != null && movieObject.FilePath != null && uploadRequest != null)
            {
                // TODO: Figure out safer way to determine temporary filepath.
                string temporaryFilePath = DateTime.Now.ToString(CultureInfo.InvariantCulture) + movieObject.Title;

                // Creates the new movie in the database.
                using (var db = new RentItContext())
                {
                    var newMovie = new Movie
                        {
                            Description = movieObject.Description,
                            Genre = movieObject.Genre,
                            Title = movieObject.Title,
                            FilePath = temporaryFilePath
                        };
                    db.Movies.Add(newMovie);
                    db.SaveChanges();
                    var tempMovie = db.Movies.First(m => m.FilePath == temporaryFilePath);
                    temporaryFilePath = tempMovie.ID.ToString(CultureInfo.InvariantCulture);
                }

                // Attempts to upload the file to the server.
                try
                {
                    // New file name will simply be the id in the database and 
                    string newFileName = temporaryFilePath + "_" + Path.GetExtension(uploadRequest.FileName);
                    string filePath = Path.Combine(Constants.UploadDownloadFileFolder, newFileName);

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
                    using (var db = new RentItContext())
                    {
                        var movie = db.Movies.First(m => m.FilePath == temporaryFilePath);
                        db.Movies.Remove(movie);
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