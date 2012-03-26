//-------------------------------------------------------------------------------------------------
// <copyright file="IDownloadService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.ServiceModel;

    using RentItService.Entities;
    using RentItService.Library;

    /// <summary>
    /// Interface of the download service.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    [ServiceContract]
    public interface IDownloadService
    {
        /// <summary>
        /// Creates a stream for downloading a file from the server.
        /// </summary>
        /// <param name="token">
        /// The user token.
        /// </param>
        /// <param name="downloadRequest">
        /// The movie to download.
        /// </param>
        /// <returns>
        /// The stream information necessary for download.
        /// </returns>
        /// <author>Jakob Melnyk</author>
        [OperationContract]
        RemoteFileStream DownloadFile(string token, Movie downloadRequest);
    }
}