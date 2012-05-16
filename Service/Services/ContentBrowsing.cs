//-------------------------------------------------------------------------------------------------
// <copyright file="ContentBrowsing.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System.Collections.Generic;
    using Entities;
    using Enums;
    using Interfaces;

    /// <summary>
    /// The movie information service.
    /// </summary>
    public partial class Service : IContentBrowsing
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
        public bool GetMovies(string token, out IEnumerable<Movie> movies, MovieSorting sorting = MovieSorting.Default, string genre = null, int limit = 0)
        {
            if (token == null || User.GetByToken(token) == null || limit < 0)
            {
                movies = null;
                return false;
            }

            movies = Movie.GetMovies(sorting, genre, limit);

            return true;
        }

        /// <summary>
        /// Get all genres.
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="genres">The found genres</param>
        /// <returns>Wether the request succeeded or not</returns>
        public bool GetGenres(string token, out IEnumerable<string> genres)
        {
            if (token == null || User.GetByToken(token) == null)
            {
                genres = null;
                return false;
            }

            genres = Genre.All;

            return true;
        }

        /// <summary>
        /// Searches for movies
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="query">The string to search for</param>
        /// <param name="movies">The found movies</param>
        /// <returns>Wether the request succeeded or not</returns>
        public bool Search(string token, string query, out IEnumerable<Movie> movies)
        {
            if (token == null || User.GetByToken(token) == null || query == null)
            {
                movies = null;
                return false;
            }

            movies = Movie.Search(query);

            return true;
        }

        /// <summary>
        /// Get information abouta movie
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="movie">The movie to find information about. Should have an ID.</param>
        /// <returns>Wether the request succeeded or not</returns>
        public bool GetMovieInformation(string token, ref Movie movie)
        {
            if (token == null || movie == null)
            {
                return false;
            }

            var user = User.GetByToken(token);
            if (user == null)
            {
                return false;
            }

            movie = Movie.Get(user, movie.ID);
            return true;
        }
    }
}