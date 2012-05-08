//-------------------------------------------------------------------------------------------------
// <copyright file="EditionMap.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Entities;

    /// <summary>
    /// Mapping of the Movie entity.
    /// </summary>
    public class EditionMap : EntityTypeConfiguration<Edition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditionMap"/> class.
        /// </summary>
        public EditionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.FilePath)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Edition");
            this.Property(t => t.ID).HasColumnName("edition_id");
            this.Property(t => t.MovieID).HasColumnName("movie_id");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.FilePath).HasColumnName("file_path");

            // Relationships
            this.HasRequired(t => t.Movie)
                .WithMany(t => t.Editions)
                .HasForeignKey(d => d.MovieID);
        }
    }
}