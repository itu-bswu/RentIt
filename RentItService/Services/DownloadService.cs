//-------------------------------------------------------------------------------------------------
// <copyright file="DownloadService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Diagnostics.Contracts;
    using Entities;
    using FunctionClasses;
    using Interfaces;
    using Library;

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
        public RemoteFileStream DownloadFile(string token, Edition downloadRequest)
        {
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<ArgumentNullException>(downloadRequest != null);

            return UploadDownload.DownloadFile(token, downloadRequest);
        }
    }
}