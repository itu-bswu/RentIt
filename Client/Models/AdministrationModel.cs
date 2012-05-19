// -----------------------------------------------------------------------
// <copyright file="AdministrationModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.Models
{
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Windows;

    using RentItService;

    /// <summary>
    /// Contains the logic for administrative users of the client.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    public static class AdministrationModel
    {
        #region Deletetion methods
        /// <summary>
        /// Deletes a movie from the service.
        /// </summary>
        /// <param name="movieObject">The movie to delete.</param>
        /// <returns>True if deletion was successful, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool DeleteMovie(Movie movieObject)
        {
            Contract.Requires(movieObject != null);

            return ServiceClients.ContentManagement.DeleteMovie(AccessModel.LoggedIn.Token, movieObject);
        }

        /// <summary>
        /// Deletes an edition from a movie.
        /// </summary>
        /// <param name="edition">The edition to delete.</param>
        /// <returns>True if deletion was successful, false if not.</returns>
        public static bool DeleteEdition(Edition edition)
        {
            Contract.Requires(edition != null);

            return ServiceClients.ContentManagement.DeleteEdition(AccessModel.LoggedIn.Token, edition);
        }
        #endregion

        #region Management methods

        /// <summary>
        /// Edits a movie on the service.
        /// </summary>
        /// <param name="movieObject">The movie object containing the new information.</param>
        /// <param name="newMovie">The updated movie object.</param>
        /// <returns>True if edit was successful, false if it was not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool EditMovie(Movie movieObject, out Movie newMovie)
        {
            Contract.Requires(movieObject != null);

            var ret = ServiceClients.ContentManagement.EditMovie(AccessModel.LoggedIn.Token, ref movieObject);
            newMovie = movieObject;
            return ret;
        }

        /// <summary>
        /// Registers a movie.
        /// </summary>
        /// <param name="movieObject">The movie to register.</param>
        /// <returns>True if movie was successfully registered, false if not.</returns>
        public static bool RegisterMovie(ref Movie movieObject)
        {
            Contract.Requires(movieObject != null);

            return ServiceClients.ContentManagement.RegisterMovie(AccessModel.LoggedIn.Token, ref movieObject);
        }
        #endregion

        #region Down/Up-load methods
        /// <summary>
        /// Downloads a movie from the service.
        /// </summary>
        /// <param name="editionId">The edition Id.</param>
        /// <param name="folder">Where the file should be saved.</param>
        /// <returns>The download file.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool DownloadFile(int editionId, string folder)
        {
            Contract.Requires(folder != null);

            var edition = new Edition
                              {
                                  ID = editionId
                              };

            var fileName = "";
            long length = 0;
            var token = AccessModel.LoggedIn.Token;
            Stream stream = Stream.Null;

            ServiceClients.RentalManagement.DownloadFile(
                ref edition, ref fileName, ref length, ref token, ref stream);



            var sourceStream = stream;

            var filePath = Path.Combine(folder, fileName);

            MessageBox.Show("Saving movie at location " + filePath);

            var fi = new FileInfo(filePath);

            if (fi.Directory != null && !fi.Directory.Exists)
            {
                fi.Directory.Create();
            }

            try
            {
                using (var targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    // Read from the input stream in 65000 byte chunks
                    const int bufferLen = 8192;

                    var buffer = new byte[bufferLen];
                    int count;
                    while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                    {
                        // Save to output stream
                        targetStream.Write(buffer, 0, count);
                    }

                    targetStream.Close();
                    sourceStream.Close();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Uploads a file to the service.
        /// </summary>
        /// <param name="editionName">The edition to upload.</param>
        /// <param name="movieId">Id of the movie the edition should be attached to.</param>
        /// <param name="file">The path of the file.</param>
        /// <returns>True if the upload is successful, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool UploadEdition(string editionName, int movieId, FileInfo file)
        {
            Contract.Requires(file != null);

            var edit = new Edition
            {
                Name = editionName,
                MovieID = movieId
            };

            if (!file.Exists)
            {
                throw new FileNotFoundException("File not found", file.Name);
            }

            var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);


            var uploadStream = new RemoteFileStream
                {
                    FileByteStream = stream,
                    FileName = file.Name,
                    Length = stream.Length
                };

            var tempToken = AccessModel.LoggedIn.Token;

            ServiceClients.ContentManagement.UploadEdition(edit, uploadStream.FileName, uploadStream.Length, tempToken, uploadStream.FileByteStream);

            return true;
        }
        #endregion
    }
}