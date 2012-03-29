// -----------------------------------------------------------------------
// <copyright file="ContentService.cs" company="">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Services
{
    using System;

    using RentItService.Entities;
    using RentItService.Interfaces;

    /// <summary>
    /// Service for the content providers.
    /// </summary>
    public partial class Service : IContentService
    {
        /// <summary>
        /// Operation used to update movie information.
        /// </summary>
        /// <param name="token">The user token.</param>
        /// <param name="movieObject">The Movie object containing the ID of the movie to be changed and the updated information.</param>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        /// <author></author>
        public void EditMovieInformation(string token, Movie movieObject)
        {
            // TODO: Implement EditMovieInformation
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Deletes a movie from the service.
        /// </summary>
        /// <param name="token">The user token.</param>
        /// <param name="movieObject">The movie to be deleted.</param>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        /// <author></author>
        public void DeleteMovie(string token, Movie movieObject)
        {
            // TODO: Implement DeleteMovie
            throw new System.NotImplementedException();
        }
    }
}