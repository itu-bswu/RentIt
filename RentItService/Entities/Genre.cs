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
        /// <summary>
        /// Initializes a new instance of the <see cref="Genre"/> class.
        /// </summary>
        public Genre()
        {
            this.AssociatedMovies = new List<HasGenre>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Genre"/> class.
        /// </summary>
        /// <param name="name">the genre name</param>
        public Genre(string name) : this()
        {
            this.Name = name;
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
        /// Gets or sets the associated movies through a junction entitiy.
        /// </summary>
        public virtual ICollection<HasGenre> AssociatedMovies { get; set; }

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
    }
}