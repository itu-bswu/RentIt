// -----------------------------------------------------------------------
// <copyright file="MovieInformationModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.Models
{
    using System.Collections.Generic;
    using RentItService;

    /// <summary>
    /// Contains the logic for accessing movie information on the service.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    public class MovieInformationModel
    {
        #region MovieGetters

        /// <summary>
        /// Gets all the movies.
        /// </summary>
        /// <param name="allMovies">All the movies.</param>
        /// <returns>True if movies were collected successfully, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool AllMovies(out IEnumerable<Movie> allMovies)
        {
            Movie[] movies;
            var ret = ServiceClients.ContentBrowsing.GetMovies(out movies, AccessModel.LoggedIn.Token, MovieSorting.Newest, genre: null, limit: 0);
            allMovies = movies;
            return ret;
        }

        /// <summary>
        /// Gets the most downloaded movies.
        /// </summary>
        /// <param name="mostDownloadedMovies">The most downloaded movies.</param>
        /// <param name="genre">The genre to filter by. Default is null, meaning no filter.</param>
        /// <param name="limit">The maximum of movies to get. Default is 0, meaning all movies are requested.</param>
        /// <returns>True if movies were collected successfully, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool MostDownloaded(out IEnumerable<Movie> mostDownloadedMovies, string genre = null, int limit = 0)
        {
            Movie[] movies;
            var ret = ServiceClients.ContentBrowsing.GetMovies(out movies, AccessModel.LoggedIn.Token, MovieSorting.MostDownloaded, genre, limit);
            mostDownloadedMovies = movies;
            return ret;
        }

        /// <summary>
        /// Gets the movies with a specific genre.
        /// </summary>
        /// <param name="moviesInGenre">The movies with the specified genre.</param>
        /// <param name="genre">The genre to be filtered by.</param>
        /// <returns>True if movies were collected successfully, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool MoviesByGenre(out IEnumerable<Movie> moviesInGenre, string genre)
        {
            Movie[] movies;
            var ret = ServiceClients.ContentBrowsing.GetMovies(out movies, AccessModel.LoggedIn.Token, MovieSorting.Newest, genre, 0);
            moviesInGenre = movies;
            return ret;
        }

        /// <summary>
        /// Gets the newest movies.
        /// </summary>
        /// <param name="newestMovies">The newest movies.</param>
        /// <param name="genre">The genre to filter by. Default is null, meaning no filter.</param>
        /// <param name="limit">The maximum of movies to get. Default is 0, meaning all movies are requested.</param>
        /// <returns>True if movies were collected successfully, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool Newest(out IEnumerable<Movie> newestMovies, string genre = null, int limit = 0)
        {
            Movie[] movies;
            var ret = ServiceClients.ContentBrowsing.GetMovies(out movies, AccessModel.LoggedIn.Token, MovieSorting.Default, genre, limit);
            newestMovies = movies;
            return ret;
        }

        #endregion

        /// <summary>
        /// Gets the information about the movie.
        /// </summary>
        /// <param name="movieId">The id of the movie to get information about.</param>
        /// <param name="movieInfo">The updated Movie object.</param>
        /// <returns>True if movie information was collected successfully, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool GetMovieInfo(int movieId, out Movie movieInfo)
        {
            var m = new Movie
                        {
                            ID = movieId
                        };

            var ret = ServiceClients.ContentBrowsing.GetMovieInformation(AccessModel.LoggedIn.Token, ref m);
            movieInfo = m;
            return ret;
        }

        /// <summary>
        /// Gets all the genres.
        /// </summary>
        /// <param name="genres">All the available genres.</param>
        /// <returns>True if genre information was collected successfully, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool AllGenres(out IEnumerable<string> genres)
        {
            string[] allGenres;
            var ret = ServiceClients.ContentBrowsing.GetGenres(out allGenres, AccessModel.LoggedIn.Token);
            genres = allGenres;
            return ret;
        }

        /// <summary>
        /// Searches the movies.
        /// </summary>
        /// <param name="searchResult">The results of the search.</param>
        /// <param name="searchString">The search string.</param>
        /// <returns>True if movies were collected successfully, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool Search(out IEnumerable<Movie> searchResult, string searchString)
        {
            Movie[] movies;
            var ret = ServiceClients.ContentBrowsing.Search(out movies, AccessModel.LoggedIn.Token, searchString);
            searchResult = movies;
            return ret;
        }
    }
}