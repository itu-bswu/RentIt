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

        /// <summary>
        /// Gets the current rentals of the user.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>The current rentals of the user.</returns>
        public static IEnumerable<Rental> GetCurrentRentals(string token)
        {
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<ArgumentException>(User.GetByToken(token) != null);

            User user = User.GetByToken(token);
            DateTime newTime = DateTime.Now.AddDays(-7); // TODO: Temporary fix.

            using (var db = new RentItContext())
            {
                return db.Rentals.Where(r => r.UserID == user.ID & r.Time > newTime);
            }
        }
    }
}