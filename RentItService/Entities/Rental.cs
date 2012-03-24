//-------------------------------------------------------------------------------------------------
// <copyright file="Rental.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Entities
{
    using System;

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
        public int MovieID { get; set; }

        /// <summary>
        /// Gets or sets the time of rental.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the associated Movie entity.
        /// </summary>
        public virtual Movie Movie { get; set; }

        /// <summary>
        /// Gets or sets the associated User entity.
        /// </summary>
        public virtual User User { get; set; }
    }
}