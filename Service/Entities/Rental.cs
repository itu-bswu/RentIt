//-------------------------------------------------------------------------------------------------
// <copyright file="Rental.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Rental entity (Entity Framework POCO class).
    /// </summary>
    [DataContract(IsReference = true)]
    [KnownType(typeof(Edition))]
    [KnownType(typeof(User))]
    public class Rental
    {
        /// <summary>
        /// Gets all rentals.
        /// </summary>
        public static IEnumerable<Rental> All
        {
            get
            {
                return RentItContext.Db.Rentals;
            }
        }

        /// <summary>
        /// Gets or sets the rental ID.
        /// </summary>
        [DataMember]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user renting the movie.
        /// </summary>
        [DataMember]
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the movie getting rented out.
        /// </summary>
        [DataMember]
        public int EditionID { get; set; }

        /// <summary>
        /// Gets or sets the time of rental.
        /// </summary>
        [DataMember]
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the associated User entity.
        /// </summary>
        [DataMember]
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the associated movie edition.
        /// </summary>
        [DataMember]
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
    }
}