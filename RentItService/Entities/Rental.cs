//-------------------------------------------------------------------------------------------------
// <copyright file="Rental.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// Rental entity (Entity Framework POCO class).
    /// </summary>
    public class Rental
    {
        /// <summary>
        /// Gets or sets the rental ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user renting the movie.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the movie getting rented out.
        /// </summary>
        public int EditionID { get; set; }

        /// <summary>
        /// Gets or sets the time of rental.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the associated User entity.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the associated movie edition.
        /// </summary>
        public virtual Edition Edition { get; set; }

        /// <summary>
        /// Gets the associated Movie entity.
        /// </summary>
        public Movie Movie
        {
            get
            {
                return Edition.Movie;
            }
        }

        public static IEnumerable<Rental> All()
        {
            return RentItContext.Db.Rentals.ToList();
        }
    }
}