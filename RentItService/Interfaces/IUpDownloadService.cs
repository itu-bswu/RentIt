//-------------------------------------------------------------------------------------------------
// <copyright file="IUpDownloadService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------


namespace RentItService.Interfaces
{
    using System.ServiceModel;
    using RentItService.Library;
    using Tools;

    /// <summary>
    /// Interface of the download service.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    [ServiceContract(Namespace = Constants.UpDownloadNamespace)]
    public interface IUpDownloadService
    {
        /// <summary>
        /// Creates a stream for downloading a file from the server.
        /// </summary>
        /// <param name="downloadRequest">The download request.</param>
        /// <returns>The stream information necessary for download.</returns>
        /// <author>Jakob Melnyk</author>
        [OperationContract]
        RemoteFileStream DownloadFile(FileRequest downloadRequest);

        /// <summary>
        /// Accepts a RemoteFileStream and saves the file to the server.
        /// </summary>
        /// <param name="uploadRequest">The upload request</param>
        /// <author>Jakob Melnyk</author>
        [OperationContract]
        void UploadFile(RemoteFileStream uploadRequest);
    }
}