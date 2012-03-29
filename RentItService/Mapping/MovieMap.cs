//-------------------------------------------------------------------------------------------------
// <copyright file="MovieMap.cs" company="RentIt">
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
    public class MovieMap : EntityTypeConfiguration<Movie>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovieMap"/> class.
        /// </summary>
        public MovieMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(100);
                
            this.Property(t => t.ImagePath)
                .IsFixedLength()
                .HasMaxLength(100);
                
            this.Property(t => t.FilePath)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(100);

            this.Property(t => t.Genre)
                .IsFixedLength()
                .HasMaxLength(50);
                
            // Table & Column Mappings
            this.ToTable("Movie");
            this.Property(t => t.ID).HasColumnName("movie_id");
            this.Property(t => t.Title).HasColumnName("title");
            this.Property(t => t.Description).HasColumnName("description");
            this.Property(t => t.ImagePath).HasColumnName("image_path");
            this.Property(t => t.FilePath).HasColumnName("file_path");
            this.Property(t => t.Genre).HasColumnName("genre");
        }
    }
}