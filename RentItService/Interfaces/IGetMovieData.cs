// -----------------------------------------------------------------------
// <copyright file="IGetMovieData.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using Entities;

    /// <summary>
    /// Service contract used to get information about movies.
    /// </summary>
    [ServiceContract]
    public interface IGetMovieData
    {
        /// <summary>
        /// Gets information about a specific movie.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="movieId">The ID of the movie to get the information of.</param>
        /// <returns>A movie object equivalent to the entry in the database.</returns>
        /// <author>Jacob Grooss</author>
        [OperationContract]
        Movie GetMovieInformation(string token, int movieId);

        /// <summary>
        /// Gets the most downloaded movies.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>An IEnumerable containing the most downloaded movies.</returns>
        [OperationContract]
        IEnumerable<Movie> GetMostDownloaded(string token);

        /// <summary>
        /// Gets the newest movies.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="limit">Amount of results to get (0 for unlimited).</param>
        /// <returns>An IEnumerable containing the newest added movies.</returns>
        [OperationContract]
        IEnumerable<Movie> GetNewest(string token, int limit = 0);

        /// <summary>
        /// Gets all the genres currently applied to the movies in the database.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>An IEnumerable containing all the genres in the database.</returns>
        [OperationContract]
        IEnumerable<string> GetAllGenres(string token);

        /// <summary>
        /// Filters the list of movies into a particular genre.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="genre">The genre to filter by.</param>
        /// <returns>An IEnumerable containing the filtered movies.</returns>
        [OperationContract]
        IEnumerable<Movie> GetMoviesByGenre(string token, string genre);

        /// <summary>
        /// Searches the database for a specific movie title.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="search">The search string.</param>
        /// <returns>An IEnumerable containing the movies fitting the search.</returns>
        [OperationContract]
        IEnumerable<Movie> Search(string token, string search);

        /// <summary>
        /// Finds all the movies in the database and returns them.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>All the active movies in the database.</returns>
        [OperationContract]
        IEnumerable<Movie> GetAllMovies(string token);
    }
}