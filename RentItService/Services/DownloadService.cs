//-------------------------------------------------------------------------------------------------
// <copyright file="DownloadService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using Entities;
    using Interfaces;
    using Library;
    using Tools;

    /// <summary>
    /// The download service class.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    public partial class Service : IDownloadService
    {
        /// <summary>
        /// Creates a stream for downloading a file from the server. 
        /// The movie is identified by the ID in the instance of the Movie class.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="downloadRequest">The movie to download.</param>
        /// <returns>The stream information necessary for download.</returns>
        /// <author>Jakob Melnyk</author>
        public RemoteFileStream DownloadFile(string token, Movie downloadRequest)
        {
            using (var db = new RentItContext())
            {
                string filePath;

                var movie = db.Movies.First(m => m.ID == downloadRequest.ID);
                if (movie.Genre == downloadRequest.Genre
                    & movie.Description == downloadRequest.Description
                    & movie.Title == downloadRequest.Title)
                {
                    filePath = Path.Combine(Constants.UploadDownloadFileFolder, movie.FilePath);
                }
                else
                {
                    throw new Exception(); // TODO: Throw better exception.
                }

                try
                {
                    FileInfo fileInfo = new FileInfo(filePath);

                    // Check to see if file exists.
                    if (!fileInfo.Exists)
                    {
                        throw new FileNotFoundException("File not found", downloadRequest.FilePath);
                    }

                    // Open stream
                    FileStream stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);

                    // Set up rfs
                    return new RemoteFileStream(movie.FilePath, fileInfo.Length, stream);
                }
                catch (Exception e)
                {
                    throw new Exception("Could not create the stream.", e); // TODO: Why catch and throw another exception?
                }
            }
        }
    }
}