//-------------------------------------------------------------------------------------------------
// <copyright file="Genre.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Entities
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Genre entity (Entity Framework POCO class).
    /// </summary>
    public class Genre
    {
        #region Constructor(s)
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Genre"/> class.
        /// </summary>
        public Genre()
        {
            this.AssociatedMovies = new List<Movie>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Genre"/> class.
        /// </summary>
        /// <param name="name">The genre name</param>
        public Genre(string name) : this()
        {
            this.Name = name;
        }

        #endregion Constructor(s)

        #region Properties

        /// <summary>
        /// Gets or sets the genres's ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the genre name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the associated movies.
        /// </summary>
        public virtual ICollection<Movie> AssociatedMovies { get; set; }

        #endregion Properties

        #region Helpers

        /// <summary>
        /// Returns the genre with the given name, creates it if it doesn't exist.
        /// </summary>
        /// <param name="name">Genre name</param>
        /// <returns>The Genre</returns>
        public static Genre GetOrCreateGenre(string name)
        {
            using (var db = new RentItContext())
            {
                var genres = db.Genres.Where(g => g.Name.Equals(name));

                if (!genres.Any())
                {
                    var genreObj = new Genre(name);
                    db.Genres.Add(genreObj);
                    return genreObj;
                }

                return genres.First();
            }
        }

        #endregion Helpers

        #region Overrides

        /// <summary>
        /// Determines whether or not two objects of type Genre are equal.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True for equals; false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (this == obj)
            {
                return true;
            }

            var other = obj as Genre;
            if (other == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.Name == other.Name);
        }

        /// <summary>
        /// Generate hashcode for the genre.
        /// </summary>
        /// <returns>Hashcode based on genre name.</returns>
        public override int GetHashCode()
        {
            return (this.Name != null ? this.Name.GetHashCode() : 0);
        }

        #endregion Overrides
    }
}