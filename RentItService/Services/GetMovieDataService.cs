//-------------------------------------------------------------------------------------------------
// <copyright file="GetMovieDataService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using Entities;
    using Interfaces;

    using RentItService.Exceptions;

    /// <summary>
    /// The movie information service.
    /// </summary>
    public partial class Service : IGetMovieData
    {
        /// <summary>Gets information about a specific movie.</summary>
        /// <param name="token">The session token.</param>
        /// <param name="movieId">The ID of the movie to get.</param>
        /// <returns>A movie object equivalent to the entry in the database.</returns>
        public Movie GetMovieInformation(string token, int movieId)
        {
            Contract.Requires(token != null);
            Contract.Requires<UserNotFoundException>(User.GetByToken(token) != null);

            User user = User.GetByToken(token);

            using (var db = new RentItContext())
            {
                return Enumerable.FirstOrDefault(db.Movies, movie => movie.ID == movieId);
            }
        }

        /// <summary>
        /// Gets the most downloaded movies.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>An IEnumerable containing the most downloaded movies.</returns>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        public IEnumerable<Movie> GetMostDownloaded(string token)
        {
            // TODO: Implement GetMostDownloaded
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all the genres currently applied to the movies in the database.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>An IEnumerable containing all the genres in the database.</returns>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        public IEnumerable<string> GetAllGenres(string token)
        {
            // TODO: Implement GetAllGenres
            throw new NotImplementedException();
        }

        /// <summary>
        /// Filters the list of movies into a particular genre.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="genre">The genre to filter by.</param>
        /// <returns>An IEnumerable containing the filtered movies.</returns>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        public IEnumerable<Movie> GetMoviesByGenre(string token, string genre)
        {
            // TODO: Implement GetMoviesByGenre
            throw new NotImplementedException();
        }

        /// <summary>
        /// Searches the database for a specific movie title.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="search">The search string.</param>
        /// <returns>An IEnumerable containing the movies fitting the search.</returns>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        public IEnumerable<Movie> Search(string token, string search)
        {
            // TODO: Implement Search
            throw new NotImplementedException();
        }
    }
}