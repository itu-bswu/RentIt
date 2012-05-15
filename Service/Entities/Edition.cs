﻿//-------------------------------------------------------------------------------------------------
// <copyright file="Edition.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Movie edition entity (Entity Framework POCO class).
    /// </summary>
    public class Edition
    {
        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="Edition"/> class.
        /// </summary>
        public Edition()
        {
            this.Rentals = new List<Rental>();
        }

        #endregion Constructor(s)

        #region Properties
        
        /// <summary>
        /// Gets all editions.
        /// </summary>
        public static IEnumerable<Edition> All
        {
            get
            {
                return RentItContext.Db.Editions;
            }
        }

        /// <summary>
        /// Gets or sets the ID of the movie edition.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the movie edition.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the filepath of the movie edition.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated movie.
        /// </summary>
        public int MovieID { get; set; }

        /// <summary>
        /// Gets or sets the movie.
        /// </summary>
        public virtual Movie Movie { get; set; }

        /// <summary>
        /// Gets or sets a list of rentals for this movie edition.
        /// </summary>
        public virtual ICollection<Rental> Rentals { get; set; }

        #endregion Properties
    }
}