// -----------------------------------------------------------------------
// <copyright file="IContentService.cs" company="">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.ServiceModel;
    using Entities;

    /// <summary>
    /// Service contract for the content provider operations.
    /// </summary>
    [ServiceContract]
    public interface IContentService
    {
        /// <summary>
        /// Update information for a movie. 
        /// The movie is identified by the ID in the instance of the Movie class. 
        /// The other information is updated with the new values.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="movieObject">The Movie object containing the ID of the movie to be changed and the updated information.</param>
        /// <author>Jacob Grooss</author>
        [OperationContract]
        void EditMovieInformation(string token, Movie movieObject);

        /// <summary>
        /// Deletes a movie from the service. 
        /// The movie is identified by the ID in the instance of the Movie class. 
        /// The other properties in the Movie instance are ignored.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="movieObject">The movie to be deleted.</param>
        /// <author>Jakob Melnyk</author>
        [OperationContract]
        void DeleteMovie(string token, Movie movieObject);
    }
}