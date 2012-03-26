//-------------------------------------------------------------------------------------------------
// <copyright file="UploadService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System.IO;

    using Contracts.Interfaces;
    using Contracts.Library;

    using Tools;

    /// <summary>
    /// The RentIt service class.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    public partial class Service : IUploadService
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
        public void UploadFile(string token, RemoteFileStream uploadRequest, string movieObject)
        {
            FileStream targetStream;
            Stream sourceStream = uploadRequest.FileByteStream;

            string filePath = Path.Combine(Constants.UploadDownloadFileFolder, uploadRequest.FileName);

            using (targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                const int BufferLength = 8192;
                byte[] buffer = new byte[BufferLength];
                int count;
                while ((count = sourceStream.Read(buffer, 0, BufferLength)) > 0)
                {
                    targetStream.Write(buffer, 0, count);
                }

                targetStream.Close();
                sourceStream.Close();
            }
        }
    }
}