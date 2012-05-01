//-------------------------------------------------------------------------------------------------
// <copyright file="GenreMap.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Entities;

    /// <summary>
    /// Database mapping for genres.
    /// </summary>
    public class GenreMap : EntityTypeConfiguration<Genre>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenreMap"/> class.
        /// </summary>
        public GenreMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Genre");
            this.Property(t => t.ID).HasColumnName("genre_id");
            this.Property(t => t.Name).HasColumnName("name");
        }
    }
}