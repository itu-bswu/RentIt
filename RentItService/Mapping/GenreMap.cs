//-------------------------------------------------------------------------------------------------
// <copyright file="GenreMap.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Entities;

    public class GenreMap : EntityTypeConfiguration<Genre>
    {
        public GenreMap()
        {
            this.HasKey(t => t.ID);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.ToTable("Genre");
            this.Property(t => t.ID).HasColumnName("genre_id");
            this.Property(t => t.Name).HasColumnName("name");
        }
    }
}