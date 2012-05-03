// -----------------------------------------------------------------------
// <copyright file="AdministrationModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.Models
{
    using System.Collections.Generic;
    using System.IO;

    using RentItService;

    /// <summary>
    /// Contains the logic for administrative users of the client.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    public static class AdministrationModel
    {
        /// <summary>
        /// Deletes a movie from the service.
        /// </summary>
        /// <param name="movieObject">The movie to delete.</param>
        /// <author>Jakob Melnyk</author>
        public static void DeleteMovie(Movie movieObject)
        {
            ServiceClients.Csc.DeleteMovie(AccessModel.LoggedIn.Token, movieObject);
        }

        /// <summary>
        /// Edits a movie on the service.
        /// </summary>
        /// <param name="movieObject">The movie object containing the new information.</param>
        /// <author>Jakob Melnyk</author>
        public static void EditMovie(Movie movieObject)
        {
            ServiceClients.Csc.EditMovieInformation(AccessModel.LoggedIn.Token, movieObject);
        }

        /// <summary>
        /// Downloads a movie from the service.
        /// </summary>
        /// <param name="movieId">The ID of the movie to be downloaded.</param>
        /// <param name="folder">Where the file should be saved.</param>
        /// <author>Jakob Melnyk</author>
        public static void DownloadFile(int movieId, string folder)
        {
            var m = new Movie
                {
                    ID = movieId
                };

            var remoteDownloadStream = ServiceClients.Dsc.DownloadFile(AccessModel.LoggedIn.Token, m);
            FileStream targetStream;
            var sourceStream = remoteDownloadStream.FileByteStream;

            var filePath = folder + remoteDownloadStream.FileName;

            var fi = new FileInfo(filePath);

            if (fi.Directory != null && !fi.Directory.Exists)
            {
                fi.Directory.Create();
            }

            using (targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // Read from the input stream in 65000 byte chunks
                const int BufferLen = 65000;

                var buffer = new byte[BufferLen];
                int count;
                while ((count = sourceStream.Read(buffer, 0, BufferLen)) > 0)
                {
                    // Save to output stream
                    targetStream.Write(buffer, 0, count);
                }

                targetStream.Close();
                sourceStream.Close();
            }
        }

        /// <summary>
        /// Uploads a file to the service.
        /// </summary>
        /// <param name="movieObject">The movie to upload.</param>
        /// <param name="file">The path of the file.</param>
        /// <returns>True if the upload is successful, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool UploadMovie(Movie movieObject, FileInfo file)
        {
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

            ServiceClients.Usc.UploadFile(AccessModel.LoggedIn.Token, uploadStream, movieObject);
            return false;
        }

        /// <summary>
        /// Gets the content publishers on the service.
        /// </summary>
        /// <returns>All the content publishers on the service.</returns>
        /// <author>Jakob Melnyk</author>
        public static IEnumerable<User> ContentPublishers()
        {
            return ServiceClients.Uic.GetContentPublishers(AccessModel.LoggedIn.Token);
        }

        /// <summary>
        /// Gets the users on the service.
        /// </summary>
        /// <returns>All the users on the service.</returns>
        /// <author>Jakob Melnyk</author>
        public static IEnumerable<User> Users()
        {
            return ServiceClients.Uic.GetUsers(AccessModel.LoggedIn.Token);
        }
    }
}