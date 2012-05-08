//-------------------------------------------------------------------------------------------------
// <copyright file="RentalMap.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Entities;

    /// <summary>
    /// Mapping of the Rental entity.
    /// </summary>
    public class RentalMap : EntityTypeConfiguration<Rental>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentalMap"/> class.
        /// </summary>
        public RentalMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Rental");
            this.Property(t => t.ID).HasColumnName("rental_id");
            this.Property(t => t.UserID).HasColumnName("user_id");
            this.Property(t => t.EditionID).HasColumnName("edition_id");
            this.Property(t => t.Time).HasColumnName("time");

            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany(t => t.Rentals)
                .HasForeignKey(d => d.EditionID);
                
            this.HasRequired(t => t.User)
                .WithMany(t => t.Rentals)
                .HasForeignKey(d => d.UserID);
        }
    }
}