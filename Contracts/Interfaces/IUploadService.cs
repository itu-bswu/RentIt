//-------------------------------------------------------------------------------------------------
// <copyright file="IUploadService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Contracts.Interfaces
{
    using System.ServiceModel;

    using Contracts.Library;

    /// <summary>
    /// Contract for the UploadService
    /// </summary>
    /// <author>Jakob Melnyk</author>
    [ServiceContract]
    public interface IUploadService
    {
        /// <summary>
        /// Used to upload a file to the database.
        /// </summary>
        /// <param name="token">
        /// The user token.
        /// </param>
        /// <param name="uploadRequest">
        /// The RemoteFileStream to upload.
        /// </param>
        /// <param name="movieObject">
        /// The movie object.
        /// </param>
        /// <author>Jakob Melnyk</author>
        void UploadFile(string token, RemoteFileStream uploadRequest, string movieObject);
    }
}
