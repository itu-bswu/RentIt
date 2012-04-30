//-------------------------------------------------------------------------------------------------
// <copyright file="HasGenreMap.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Entities;

    /// <summary>
    /// Mapping of the HasGenre entity.
    /// </summary>
    public class HasGenreMap : EntityTypeConfiguration<HasGenre>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HasGenreMap"/> class.
        /// </summary>
        public HasGenreMap()
        {
            this.HasKey(t => t.ID);

            this.ToTable("HasGenre");
            this.Property(t => t.ID).HasColumnName("hasgenre_id");
            this.Property(t => t.GenreId).HasColumnName("genre_id");
            this.Property(t => t.MovieId).HasColumnName("movie_id");

            // Relationships
            this.HasRequired(t => t.Movie)
                .WithMany(t => t.HasGenres)
                .HasForeignKey(d => d.MovieId);

            this.HasRequired(t => t.Genre)
                .WithMany(t => t.AssociatedMovies)
                .HasForeignKey(d => d.GenreId);
        }
    }
}