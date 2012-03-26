// -----------------------------------------------------------------------
// <copyright file="MovieService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Services
{
    using System.Linq;

    using RentItService.Entities;
    using RentItService.Interfaces;

    /// <summary>
    /// A class that exposes services concerning the Movie
    /// part of the data model
    /// </summary>
    public class MovieService : IMovieService
    {
        /// <summary>
        /// The context from which the entities are taken
        /// </summary>
        private RentItContext context = new RentItContext();

        /// <summary>
        /// Gets the information about a movie using the movie id 
        /// </summary>
        /// <param name="id">
        /// The id to look for
        /// </param>
        /// <returns>
        /// The movie, which holds all information about itself
        /// </returns>
        public Movie GetMovieInformation(int id)
        {
            return Enumerable.FirstOrDefault(this.context.Movies, movie => movie.ID == id);
        }

        /// <summary>
        /// Gets the information about a movie using the movie id 
        /// </summary>
        /// <param name="movieTitle">
        /// The title to look for
        /// </param>
        /// <returns>
        /// The movie, which holds all information about itself
        /// </returns>
        public Movie GetMovieInformation(string movieTitle)
        {
            return Enumerable.FirstOrDefault(this.context.Movies, movie => movie.Title == movieTitle);
        }
    }
}