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
        /// Gets all genres.
        /// </summary>
        public static IEnumerable<string> All
        {
            get
            {
                return RentItContext.Db.Genres.Select(genre => genre.Name);
            }
        }

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
        /// Gets wether a genre exists in the database or not
        /// </summary>
        /// <param name="name">The genre name</param>
        /// <returns>true if it exists, false otherwise</returns>
        public static bool HasGenre(string name)
        {
            return All.Any(g => g.Equals(name));
        }

        /// <summary>
        /// Returns the genre with the given name, creates it if it doesn't exist.
        /// </summary>
        /// <param name="name">Genre name</param>
        /// <returns>The Genre</returns>
        public static Genre GetOrCreateGenre(string name)
        {
            var genres = RentItContext.Db.Genres.Where(g => g.Name.Equals(name));

            if (!genres.Any())
            {
                var genreObj = new Genre(name);

                RentItContext.Db.Genres.Add(genreObj);
                RentItContext.Db.SaveChanges();

                return genreObj;
            }

            return genres.Single();
        }

        #endregion Helpers

        #region Overrides

        /// <summary>
        /// Allows implicit casting to string
        /// </summary>
        /// <param name="genre">The genre to cast</param>
        /// <returns>The genre's name</returns>
        public static implicit operator string(Genre genre)
        {
            return genre.Name;
        }

        /// <summary>
        /// Equals operator overloading.
        /// </summary>
        /// <param name="a">Genre A</param>
        /// <param name="b">Genre B</param>
        /// <returns>True if equals; false otherwise</returns>
        public static bool operator ==(Genre a, Genre b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Equals(b);
        }

        /// <summary>
        /// Not equals operator overloading.
        /// </summary>
        /// <param name="a">Genre A</param>
        /// <param name="b">Genre B</param>
        /// <returns>True if not equals; false otherwise</returns>
        public static bool operator !=(Genre a, Genre b)
        {
            return !(a == b);
        }

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

            if (ReferenceEquals(this, obj))
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