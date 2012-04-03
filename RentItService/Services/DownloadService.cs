//-------------------------------------------------------------------------------------------------
// <copyright file="DownloadService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
    using Entities;
    using Interfaces;
    using Library;

    using RentItService.FunctionClasses;

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
            Contract.Requires<ArgumentNullException>(token != null);

            Contract.Requires<ArgumentNullException>(downloadRequest != null);
            Contract.Requires(downloadRequest.Genre != null &
                              downloadRequest.Description != null &
                              downloadRequest.Title != null);

            return UploadDownload.DownloadFile(token, downloadRequest);
        }
    }
}