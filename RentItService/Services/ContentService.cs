// -----------------------------------------------------------------------
// <copyright file="ContentService.cs" company="">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using Entities;
    using Interfaces;

    /// <summary>
    /// Service for the content providers.
    /// </summary>
    public partial class Service : IContentService
    {
        /// <summary>
        /// Update information for a movie. 
        /// The movie is identified by the ID in the instance of the Movie class. 
        /// The other information is updated with the new values.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="movieObject">The Movie object containing the ID of the movie to be changed and the updated information.</param>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        /// <author></author>
        public void EditMovieInformation(string token, Movie movieObject)
        {
            // TODO: Implement EditMovieInformation
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a movie from the service. 
        /// The movie is identified by the ID in the instance of the Movie class. 
        /// The other properties in the Movie instance are ignored.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="movieObject">The movie to be deleted.</param>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        /// <author></author>
        public void DeleteMovie(string token, Movie movieObject)
        {
            // TODO: Implement DeleteMovie
            throw new NotImplementedException();
        }
    }
}