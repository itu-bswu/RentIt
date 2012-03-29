//-------------------------------------------------------------------------------------------------
// <copyright file="RentITContext.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Entities;
    using Mapping;

    /// <summary>
    /// Database context (Entity Framework).
    /// </summary>
    public class RentItContext : DbContext
    {
        /// <summary>
        /// Initializes static members of the <see cref="RentItContext"/> class.
        /// </summary>
        static RentItContext()
        {
            Database.SetInitializer<RentItContext>(null);
        }

        /// <summary>
        /// Gets or sets all users of RentIt.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets all movies in RentIt.
        /// </summary>
        public DbSet<Movie> Movies { get; set; }

        /// <summary>
        /// Gets or sets a set of rentals in RentIt.
        /// </summary>
        public DbSet<Rental> Rentals { get; set; }

        /// <summary>
        /// Model configuration.
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Configurations.Add(new MovieMap());
            modelBuilder.Configurations.Add(new RentalMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}