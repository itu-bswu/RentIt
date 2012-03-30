//-------------------------------------------------------------------------------------------------
// <copyright file="IUploadService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.ServiceModel;
    using Entities;
    using Library;

    /// <summary>
    /// Contract for the upload service.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    [ServiceContract]
    public interface IUploadService
    {
        /// <summary>
        /// Upload a new media file, and add a new movie with that file.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="uploadRequest">The RemoteFileStream to upload.</param>
		/// <param name="movieObject">The movie object with the movie information.</param>
        /// <returns>True if upload was successful, false if not.</returns>
        [OperationContract]
        bool UploadFile(string token, RemoteFileStream uploadRequest, Movie movieObject);
    }
}
