﻿//-------------------------------------------------------------------------------------------------
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
    using Enums;
    using Exceptions;
    using Interfaces;

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
            return Movie.Get(token, movieId);
        }

        /// <summary>
        /// Gets the most downloaded movies.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="limit">The maximum number of entries to return.</param>
        /// <returns>An IEnumerable containing the most downloaded movies.</returns>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        public IEnumerable<Movie> GetMostDownloaded(string token, int limit = 0)
        {
            Contract.Requires<ArgumentNullException>(token != null);

            return Movie.MostDownloaded(token, limit);
        }

        /// <summary>
        /// Gets the newest released movies.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="limit">Amount of results to get (0 for unlimited).</param>
        /// <returns>An IEnumerable containing the newest released movies.</returns>
        public IEnumerable<Movie> GetNewest(string token, int limit = 0)
        {
            User.GetByToken(token);

            return Movie.Newest(limit);
        }

        /// <summary>
        /// Gets all the genres currently applied to the movies in the database.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>An IEnumerable containing all the genres in the database.</returns>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        public IEnumerable<Genre> GetAllGenres(string token)
        {
            Contract.Requires<InsufficientRightsException>(User.GetByToken(token).Type == UserType.SystemAdmin);

            return Movie.GetAllGenres();
        }

        /// <summary>
        /// Filters the list of movies into a particular genre.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="genre">The genre to filter by.</param>
        /// <returns>An IEnumerable containing the filtered movies.</returns>
        public IEnumerable<Movie> GetMoviesByGenre(string token, Genre genre)
        {
            Contract.Requires<InsufficientRightsException>(User.GetByToken(token).Type == UserType.SystemAdmin);

            return Movie.ByGenre(genre);
        }

        /// <summary>
        /// Searches the database for a specific movie title.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="search">The search string.</param>
        /// <param name="limit">The maximum number of entries to return.</param>
        /// <returns>An IEnumerable containing the movies fitting the search.</returns>
        public IEnumerable<Movie> Search(string token, string search, int limit = 0)
        {
            Contract.Requires<InsufficientRightsException>(User.GetByToken(token).Type == UserType.User);

            return Movie.Search(search, limit);
        }

        /// <summary>
        /// Finds all the movies in the database and returns them.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>All the active movies in the database.</returns>
        public IEnumerable<Movie> GetAllMovies(string token)
        {
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<ArgumentException>(User.GetByToken(token) != null);

            return Movie.GetAllMovies(token);
        }
    }
}