//-------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Entities
{
    using System.Collections.Generic;
    using Enums;

    /// <summary>
    /// User entity (Entity Framework POCO class).
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            this.Rentals = new List<Rental>();
        }

        /// <summary>
        /// Gets or sets the user's ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// TODO: Hashing in setter (Salt?)
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's full name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the user's type as an integer.
        /// </summary>
        public short TypeValue { get; set; }

        /// <summary>
        /// Gets or sets the user's type.
        /// </summary>
        public UserType Type
        {
            get
            {
                return (UserType)this.TypeValue;
            }

            set
            {
                this.TypeValue = (short)value;
            }
        }

        /// <summary>
        /// Gets or sets the user's login token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets a list of the user's rentals.
        /// </summary>
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}