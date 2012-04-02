//-------------------------------------------------------------------------------------------------
// <copyright file="UploadService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using Entities;
    using Enums;
    using Interfaces;
    using Library;

    using RentItService.Exceptions;
    using RentItService.FunctionClasses;

    using Tools;

    /// <summary>
    /// The RentIt service class.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    public partial class Service : IUploadService
    {
        /// <summary>
        /// Upload a new media file, and add a new movie with that file.
        /// </summary>
        /// <param name="token">The user token.</param>
        /// <param name="uploadRequest">The RemoteFileStream to upload.</param>
        /// <param name="movieObject">The movie object with the movie information.</param>
        /// <returns>True if upload was successful, false if not.</returns>
        public bool UploadFile(string token, RemoteFileStream uploadRequest, Movie movieObject)
        {
            Contract.Requires<NullReferenceException>(token != null);

            Contract.Requires<NullReferenceException>(uploadRequest != null);
            Contract.Requires<NullReferenceException>(
                uploadRequest.FileByteStream != null & uploadRequest.FileName != null);

            Contract.Requires<NullReferenceException>(movieObject != null);
            Contract.Requires<NullReferenceException>(
                movieObject.Description != null & movieObject.Genre != null & movieObject.Title != null);

            Contract.Requires<InsufficientAccessLevelException>(User.GetByToken(token).Type == UserType.ContentProvider);

            return UploadDownload.UploadFile(token, uploadRequest, movieObject);
        }
    }
}