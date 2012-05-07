// -----------------------------------------------------------------------
// <copyright file="Movie.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace RentItClient.Types
{
    using System;

    /// <summary>
    /// Class describing 
    /// </summary>
    public class Movie
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Movie"/> class.
        /// </summary>
        /// <param name="id">The id of the movie.</param>
        /// <param name="title">The title of the movie.</param>
        /// <param name="description">The description of the movie.</param>
        /// <param name="released">The release date of the movie.</param>
        public Movie(int id, string title, string description, DateTime released)
        {
            ID = id;
            Title = title;
            Description = description;
            ReleaseDate = released;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the id of the movie.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Gets the title of the movie.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the description of the movie.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the date the movie was released.
        /// </summary>
        public DateTime ReleaseDate { get; private set; }
        #endregion

        #region Static Methods
        /// <summary>
        /// Converts a movie of the service's Movie type to the client's Movie type.
        /// </summary>
        /// <param name="movie">The movie to convert.</param>
        /// <returns>The converted movie.</returns>
        public static Movie ConvertServiceMovie(RentItService.Movie movie)
        {
            Movie result;

            if (movie.Released != null)
            {
                result = new Movie(
                movie.ID,
                movie.Title,
                movie.Title,
                movie.Released.Value);
            }
            else
            {
                result = new Movie(
                movie.ID,
                movie.Title,
                movie.Title,
                new DateTime(0001, 01, 01, 00, 00, 00));
            }

            return result;
        }
        #endregion
    }
}
