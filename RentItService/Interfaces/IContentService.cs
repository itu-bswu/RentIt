// -----------------------------------------------------------------------
// <copyright file="IContentService.cs" company="">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.ServiceModel;
    using RentItService.Entities;

    /// <summary>
    /// Service contract for the content provider operations.
    /// </summary>
    [ServiceContract]
    public interface IContentService
    {
        /// <summary>
        /// Operation used to update movie information.
        /// </summary>
        /// <param name="token">The user token.</param>
        /// <param name="movieObject">The Movie object containing the ID of the movie to be changed and the updated information.</param>
        [OperationContract]
        void EditMovieInformation(string token, Movie movieObject);

        /// <summary>
        /// Deletes a movie from the service.
        /// </summary>
        /// <param name="token">The user token.</param>
        /// <param name="movieObject">The movie to be deleted.</param>
        [OperationContract]
        void DeleteMovie(string token, Movie movieObject);
    }
}
