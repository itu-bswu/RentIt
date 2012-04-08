﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetMovieDataService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using Entities;
    using Interfaces;

    using RentItService;
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
        /// <author>Jacob Grooss</author>
        public Movie GetMovieInformation(string token, int movieId)
        {
            Contract.Requires(token != null);
            Contract.Requires<UserNotFoundException>(User.GetByToken(token) != null);

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
            Contract.Requires<ArgumentNullException>(token != null);

            return Movie.MostDownloaded(token);
        }

        /// <summary>
        /// Gets the newest movies.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="limit">Amount of results to get (0 for unlimited).</param>
        /// <returns>An IEnumerable containing the newest added movies.</returns>
        public IEnumerable<Movie> GetNewest(string token, int limit = 0)
        {
            return Movie.Newest(limit);
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
        public IEnumerable<Movie> GetMoviesByGenre(string token, string genre)
        {
            return Movie.ByGenre(token, genre);
        }

        /// <summary>
        /// Searches the database for a specific movie title.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="search">The search string.</param>
        /// <returns>An IEnumerable containing the movies fitting the search.</returns>
        public IEnumerable<Movie> Search(string token, string search)
        {
            return Movie.Search(token, search);
        }
    }
}