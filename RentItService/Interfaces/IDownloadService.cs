//-------------------------------------------------------------------------------------------------
// <copyright file="IDownloadService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System;
    using System.Diagnostics.Contracts;
    using System.ServiceModel;
    using Entities;
    using Library;

    /// <summary>
    /// Interface of the download service.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    [ServiceContract]
    public interface IDownloadService
    {
        /// <summary>
        /// Creates a stream for downloading a file from the server. 
        /// The movie is identified by the ID in the instance of the Movie class.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="downloadRequest">The movie to download.</param>
        /// <returns>The stream information necessary for download.</returns>
        [OperationContract]
        RemoteFileStream DownloadFile(string token, Movie downloadRequest);
    }
}