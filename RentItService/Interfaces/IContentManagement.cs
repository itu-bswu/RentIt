//-------------------------------------------------------------------------------------------------
// <copyright file="IContentManagement.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.ServiceModel;
    using Entities;
    using Library;

    /// <summary>
    /// Interface for content management.
    /// </summary>
    [ServiceContract]
    public interface IContentManagement
    {
        /// <summary>
        /// Registers a new movie
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="movie">The movie to register. Should have a title</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool RegisterMovie(string token, ref Movie movie);

        /// <summary>
        /// Edits a movie
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="movie">The movie to edit. Should at least have an ID</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool EditMovie(string token, ref Movie movie);

        /// <summary>
        /// Deletes a movie
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="movie">The movie to delete</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool DeleteMovie(string token, Movie movie);

        /// <summary>
        /// Uploads a movie edition
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="stream">The file stream</param>
        /// <param name="edition">The edition. Should have title and movie</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool UploadEdition(string token, RemoteFileStream stream, ref Edition edition);

        /// <summary>
        /// Deletes a movie edition
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="edition">The edition to delete</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool DeleteEdition(string token, Edition edition);
    }
}
