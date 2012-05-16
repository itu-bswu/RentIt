//-------------------------------------------------------------------------------------------------
// <copyright file="RentItContext.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Entities;
    using Mapping;

    /// <summary>
    /// Database context (Entity Framework).
    /// </summary>
    public class RentItContext : DbContext
    {
        #region Fields

        /// <summary>
        /// Thead local context
        /// </summary>
        [ThreadStatic]
        private static RentItContext db;

        #endregion Fields

        #region Constructor(s)

        /// <summary>
        /// Initializes static members of the <see cref="RentItContext"/> class.
        /// </summary>
        static RentItContext()
        {
            Database.SetInitializer<RentItContext>(null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RentItContext"/> class.
        /// </summary>
        public RentItContext()
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        #endregion Constructor(s)

        #region Context getter and reloader

        /// <summary>
        /// Gets the DbContext. Lazy loads context object
        /// </summary>
        public static RentItContext Db
        {
            get
            {
                return (db ?? (db = new RentItContext()));
            }
        }

        #endregion

        #region Entity collections

        /// <summary>
        /// Gets or sets all users of RentIt.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets all movies in RentIt.
        /// </summary>
        public DbSet<Movie> Movies { get; set; }

        /// <summary>
        /// Gets or sets all movie editions in RentIt.
        /// </summary>
        public DbSet<Edition> Editions { get; set; }

        /// <summary>
        /// Gets or sets a set of rentals in RentIt.
        /// </summary>
        public DbSet<Rental> Rentals { get; set; }

        /// <summary>
        /// Gets or sets a set of genres in RentIt.
        /// </summary>
        public DbSet<Genre> Genres { get; set; }

        #endregion Entity collections

        #region Context reloader

        /// <summary>
        /// Reloads the context;
        /// </summary>
        public static void ReloadDb()
        {
            if (db != null)
            {
                db.Dispose();
            }

            db = null;
        }

        #endregion Context reloader

        #region Configuration

        /// <summary>
        /// Model configuration.
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Configurations.Add(new GenreMap());
            modelBuilder.Configurations.Add(new MovieMap());
            modelBuilder.Configurations.Add(new EditionMap());
            modelBuilder.Configurations.Add(new RentalMap());
            modelBuilder.Configurations.Add(new UserMap());

            modelBuilder.Entity<Movie>()
                .HasMany<Genre>(x => x.Genres)
                .WithMany(a => a.AssociatedMovies)
                .Map(c =>
                         {
                             c.MapLeftKey("movie_id");
                             c.MapRightKey("genre_id");
                             c.ToTable("HasGenre");
                         });
        }

        #endregion Configuration
    }
}