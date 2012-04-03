// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetMovieData.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the GetMovieData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RentItService.Entities;
    using RentItService.Interfaces;

    /// <summary>
    /// Implements the methods needed to get movie data.
    /// </summary>
    public class GetMovieData : IGetMovieData
    {
        RentItContext dbContext = new RentItContext();

        /// <summary>
        /// Gets the information about a specific movie
        /// </summary>
        /// <param name="token">
        /// The token that's used to verify whether a used is logged
        /// in or not.
        /// </param>
        /// <param name="movieId">
        /// The movie id used to find the movie.
        /// </param>
        /// <returns>
        /// A movie object that corrosponds to the movieId that
        /// holds the movie data.
        /// </returns>
        public Movie GetMovieInformation(string token, int movieId)
        {
            User user = new User();

            if (user != null)
            {
                return Enumerable.FirstOrDefault(this.dbContext.Movies, movie => movie.ID == movieId);
            }

            return null;
        }

        public IEnumerable<Movie> GetMostDownloaded(string token)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllGenres(string token)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetMoviesByGenre(string token, string genre)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> Search(string token, string search)
        {
            throw new NotImplementedException();
        }
    }
}