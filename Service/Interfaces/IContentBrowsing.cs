// -----------------------------------------------------------------------
// <copyright file="IContentBrowsing.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using RentItService.Enums;

namespace RentItService.Interfaces
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using Entities;

    /// <summary>
    /// Interface for content browsing.
    /// </summary>
    [ServiceContract]
    public interface IContentBrowsing
    {
        /// <summary>
        /// Get all movies.
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="movies">The found movies</param>
        /// <param name="sorting">How to sort the movies</param>
        /// <param name="genre">Which genres to filter</param>
        /// <param name="limit">The maximum number of movies to return</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool GetMovies(string token, out IEnumerable<Movie> movies, MovieSorting sorting = MovieSorting.Default, string genre = null, int limit = 0);

        /// <summary>
        /// Get all genres.
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="genres">The found genres</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool GetGenres(string token, out IEnumerable<string> genres);

        /// <summary>
        /// Searches for movies
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="query">The string to search for</param>
        /// <param name="movies">The found movies</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool Search(string token, string query, out IEnumerable<Movie> movies);

        /// <summary>
        /// Get information abouta movie
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="movie">The movie to find information about. Should have an ID.</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool GetMovieInformation(string token, ref Movie movie);
    }
}