﻿//-------------------------------------------------------------------------------------------------
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
    public partial class Service : IContentManagement
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
            Contract.Requires<ArgumentNullException>(token != null);

            Contract.Requires<ArgumentNullException>(uploadRequest != null);
            Contract.Requires<ArgumentNullException>(
                uploadRequest.FileByteStream != null & uploadRequest.FileName != null);

            Contract.Requires<ArgumentNullException>(movieObject != null);
            Contract.Requires<ArgumentNullException>(
                movieObject.Description != null & movieObject.Genres.Any() & movieObject.Title != null);

            Contract.Requires<InsufficientRightsException>(User.GetByToken(token).Type == UserType.ContentProvider);

            return UploadDownload.UploadMovieFile(token, uploadRequest, movieObject);
        }
    }
}