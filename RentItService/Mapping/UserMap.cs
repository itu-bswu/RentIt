//-------------------------------------------------------------------------------------------------
// <copyright file="UserMap.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Entities;

    /// <summary>
    /// Mapping of the User entity.
    /// </summary>
    public class UserMap : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMap"/> class.
        /// </summary>
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Username)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(100);

            this.Property(t => t.Email)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(100);

            this.Property(t => t.FullName)
                .IsFixedLength()
                .HasMaxLength(100);

            this.Property(t => t.Token)
                .IsFixedLength()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.ID).HasColumnName("user_id");
            this.Property(t => t.Username).HasColumnName("username");
            this.Property(t => t.Password).HasColumnName("password");
            this.Property(t => t.Email).HasColumnName("email");
            this.Property(t => t.FullName).HasColumnName("full_name");
            this.Property(t => t.TypeValue).HasColumnName("type");
            this.Property(t => t.Token).HasColumnName("token");
        }
    }
}