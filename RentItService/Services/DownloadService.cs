//-------------------------------------------------------------------------------------------------
// <copyright file="DownloadService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.IO;

    using RentItService.Entities;
    using RentItService.Interfaces;
    using RentItService.Library;

    using Tools;

    /// <summary>
    /// The RentIt service class.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    public partial class Service : IDownloadService
    {
        /// <summary>
        /// Creates a stream for downloading a file from the server.
        /// </summary>
        /// <param name="token">
        /// The user token.
        /// </param>
        /// <param name="downloadRequest">
        /// The movie to download. TODO: Need to change this to movie from string
        /// </param>
        /// <returns>
        /// The stream information necessary for download.
        /// </returns>
        /// <author>Jakob Melnyk</author>
        public RemoteFileStream DownloadFile(string token, Movie downloadRequest)
        {
            try
            {
                string filePath = Path.Combine(Constants.UploadDownloadFileFolder, downloadRequest.FilePath);
                FileInfo fileInfo = new FileInfo(filePath);

                // Check to see if file exists.
                if (!fileInfo.Exists)
                {
                    throw new FileNotFoundException("File not found", downloadRequest.FilePath);
                }

                // Open stream
                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                // Set up rfs
                return new RemoteFileStream(downloadRequest.FilePath, fileInfo.Length, stream);
            }
            catch (Exception e)
            {
                throw new Exception("Could not create the stream.", e);
            }
        }
    }
}