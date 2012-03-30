// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetMovieDataService.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Service type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RentItService.Entities;
    using RentItService.Exceptions;
    using RentItService.Interfaces;

    /// <summary>
    /// Implements the methods necessary to get information
    /// about movies.
    /// </summary>
    public partial class Service : IGetMovieData
    {
        /// <summary>
        /// The context that grants access to the database
        /// </summary>
        private RentItContext dbContext = new RentItContext();
        
        /// <summary>Gets information about a specific movie.</summary>
        /// <param name="token">The session token.</param>
        /// <param name="movieId">The ID of the movie to get.</param>
        /// <returns>A movie object equivalent to the entry in the database.</returns>
        public Movie GetMovieInformation(string token, int movieId)
        {
            try
            {
                User user = User.GetByToken(token);

                return Enumerable.FirstOrDefault(this.dbContext.Movies, movie => movie.ID == movieId);
            }
            catch (UserNotFoundException)
            {
                return null;
            }
        }

        /// <summary>Gets the most downloaded movies.</summary>
        /// <param name="token">The session token.</param>
        /// <returns>An IEnumerable containing the most downloaded movies.</returns>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        public IEnumerable<Movie> GetMostDownloaded(string token)
        {
            // TODO: Implement GetMostDownloaded
            throw new System.NotImplementedException();
        }

        /// <summary>Gets the genres in the database.</summary>
        /// <param name="token">The session token.</param>
        /// <returns>An IEnumerable containing all the genres in the database.</returns>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        public IEnumerable<string> GetAllGenres(string token)
        {
            // TODO: Implement GetAllGenres
            throw new System.NotImplementedException();
        }

        /// <summary>Filters the list of movies into a particular genre.</summary>
        /// <param name="token">The session token.</param>
        /// <param name="genre">The genre to filter by.</param>
        /// <returns>An IEnumerable containing the filtered movies.</returns>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        public IEnumerable<Movie> GetMoviesByGenre(string token, string genre)
        {
            // TODO: Implement GetMoviesByGenre
            throw new System.NotImplementedException();
        }

        /// <summary>Searches the database for a specific movie title.</summary>
        /// <param name="token">The session token.</param>
        /// <param name="search">The search string.</param>
        /// <returns>An IEnumerable containing the movies fitting the search.</returns>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        public IEnumerable<Movie> Search(string token, string search)
        {
            // TODO: Implement Search
            throw new System.NotImplementedException();
        }
    }
}