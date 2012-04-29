// -----------------------------------------------------------------------
// <copyright file="GetMovieInformationModel.cs" company="RentIt">
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
    public class GetMovieInformationModel
    {
        /// <summary>
        /// Gets the information about the movie.
        /// </summary>
        /// <param name="movieId">The id of the movie to get information about.</param>
        /// <returns>The specified movie as a Movie object.</returns>
        /// <author>Jakob Melnyk</author>
        public static Movie GetMovieInfo(int movieId)
        {
            return ServiceClients.Gmdc.GetMovieInformation(AccessModel.LoggedIn.Token, movieId);
        }

        /// <summary>
        /// Gets all the genres.
        /// </summary>
        /// <returns>All the genres.</returns>
        /// <author>Jakob Melnyk</author>
        public static IEnumerable<string> AllGenres()
        {
            return ServiceClients.Gmdc.GetAllGenres(AccessModel.LoggedIn.Token);
        }

        /// <summary>
        /// Gets all the movies.
        /// </summary>
        /// <returns>All the movies.</returns>
        /// <author>Jakob Melnyk</author>
        public static IEnumerable<Movie> AllMovies()
        {
            return ServiceClients.Gmdc.GetAllMovies(AccessModel.LoggedIn.Token);
        }

        /// <summary>
        /// Gets the most downloaded movies.
        /// </summary>
        /// <returns>The most downloaded movies.</returns>
        /// <author>Jakob Melnyk</author>
        public static IEnumerable<Movie> MostDownloaded()
        {
            return ServiceClients.Gmdc.GetMostDownloaded(AccessModel.LoggedIn.Token);
        }

        /// <summary>
        /// Gets the movies with a specific genre.
        /// </summary>
        /// <param name="genre">The genre to be filtered by.</param>
        /// <returns>The movies with the genre.</returns>
        /// <author>Jakob Melnyk</author>
        public static IEnumerable<Movie> MoviesByGenre(string genre)
        {
            return ServiceClients.Gmdc.GetMoviesByGenre(AccessModel.LoggedIn.Token, genre);
        }

        /// <summary>
        /// Gets the newest movies.
        /// </summary>
        /// <param name="limit">The maximum of movies to get. Default is 0.</param>
        /// <returns>The newest movies within the limit.</returns>
        /// <author>Jakob Melnyk</author>
        public static IEnumerable<Movie> Newest(int limit = 0)
        {
            return ServiceClients.Gmdc.GetNewest(AccessModel.LoggedIn.Token, limit);
        }

        /// <summary>
        /// Searches the movies.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns>All the movies matching the search string.</returns>
        /// <author>Jakob Melnyk</author>
        public static IEnumerable<Movie> Search(string searchString)
        {
            return ServiceClients.Gmdc.Search(AccessModel.LoggedIn.Token, searchString);
        }
    }
}