// -----------------------------------------------------------------------
// <copyright file="UploadDownload.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItService.FunctionClasses
{
    using System;
    using System.Configuration;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
    using Entities;
    using Enums;
    using Exceptions;
    using Library;

    /// <summary>
    /// Functionality for upload and download.
    /// </summary>
    public class UploadDownload
    {
        /// <summary>
        /// Creates a stream for downloading a file from the server. 
        /// The movie is identified by the ID in the instance of the Movie class.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="downloadRequest">The movie to download.</param>
        /// <returns>The stream information necessary for download.</returns>
        /// <author>Jakob Melnyk</author>
        public static RemoteFileStream DownloadFile(string token, Edition downloadRequest)
        {
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<ArgumentNullException>(downloadRequest != null);
            Contract.Requires<ArgumentException>(downloadRequest.ID > 0);

            var user = User.GetByToken(token);
            if (!(user.Rentals.Any(x => x.EditionID == downloadRequest.ID & x.UserID == user.ID)))
            {
                throw new InsufficientRightsException();
            }

            var edition = Edition.All.First(m => m.ID == downloadRequest.ID);

            var filePath = Path.Combine(ConfigurationManager.AppSettings["BaseFilePath"], edition.FilePath);
            var fileInfo = new FileInfo(filePath);

            // Check to see if file exists.
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("File not found", fileInfo.Name);
            }

            // Open stream
            var stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);

            // Set up rfs
            return new RemoteFileStream(edition.FilePath, fileInfo.Length, stream);
        }
    }
}