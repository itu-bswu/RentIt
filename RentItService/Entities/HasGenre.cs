//-------------------------------------------------------------------------------------------------
// <copyright file="HasGenre.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Entities
{
    /// <summary>
    /// HasGenre entity (Entity Framework POCO class).
    /// </summary>
    public class HasGenre
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HasGenre"/> class.
        /// </summary>
        /// <param name="movie">The movie</param>
        /// <param name="genre">The genre</param>
        public HasGenre(Movie movie, Genre genre)
        {
            MovieId = movie.ID;
            GenreId = genre.ID;
        }

        /// <summary>
        /// Gets or sets the genres junctions's ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the genre id.
        /// </summary>
        public int GenreId { get; set; }

        /// <summary>
        /// Gets or sets the movie id.
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        public virtual Genre Genre { get; set; }

        /// <summary>
        /// Gets or sets the movie.
        /// </summary>
        public virtual Movie Movie { get; set; }
    }
}