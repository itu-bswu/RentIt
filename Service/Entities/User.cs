//-------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using Enums;
    using Exceptions;

    using Tools;
    using Tools.Encryption;

    /// <summary>
    /// User entity (Entity Framework POCO class).
    /// </summary>
    public class User
    {
        #region Fields

        /// <summary>
        /// Password salt.
        /// </summary>
        private const string Salt = "Yarrr, N0-3ye5 Gu1d30n-";

        #endregion Fields

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            this.Rentals = new List<Rental>();
        }

        #endregion Constructor(s)

        #region Properties

        /// <summary>
        /// Gets all users.
        /// </summary>
        public static IEnumerable<User> All
        {
            get
            {
                return RentItContext.Db.Users.Include("Rentals").ToList();
            }
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
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        [Pure] 
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
        /// Gets or sets a list of the movies that the user has added to the system. 
        /// (Only content providers).
        /// </summary>
        public virtual ICollection<Movie> UploadedMovies { get; set; }

        /// <summary>
        /// Gets or sets a list of the user's rentals.
        /// </summary>
        public virtual ICollection<Rental> Rentals { get; set; }

        #endregion Properties

        #region Static methods

        /// <summary>
        /// Create a new user in the database, and return that user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>The new user.</returns>
        public static User SignUp(User user)
        {
            Contract.Requires<ArgumentNullException>(user != null);
            Contract.Requires<ArgumentException>(user.Username != null);
            Contract.Requires<ArgumentException>(user.Email != null);
            Contract.Requires<ArgumentException>(user.Password != null);

            Contract.Requires<ArgumentException>(user.Username != string.Empty);
            Contract.Requires<ArgumentException>(user.Email != string.Empty && Validator.ValidateEmail(user.Email));
            
            user.ID = 0;
            user.Type = UserType.User;
            user.Token = string.Empty;
            user.Password = Hash.Sha512(user.Password + Salt);

            if (All.Any(u => u.Username == user.Username))
            {
                throw new UsernameInUseException("Username is already in use!");
            }

            RentItContext.Db.Users.Add(user);

            return RentItContext.Db.SaveChanges() > 0 ? user : null;
        }

        /// <summary>
        /// Logs the user in, and assigns a new token.
        /// </summary>
        /// <param name="username">User's username</param>
        /// <param name="password">User's password</param>
        /// <returns>User object, containing the user's token</returns>
        public static User Login(string username, string password)
        {
            Contract.Requires<ArgumentNullException>(username != null);
            Contract.Requires<ArgumentNullException>(password != null);
            Contract.Ensures(Contract.Result<User>() != null);

            password = Hash.Sha512(password + Salt);

            if (!All.ToList().Any(u => u.Username == username && u.Password == password))
            {
                throw new UserNotFoundException("No user with the given login information was found!");
            }

            var user = All.ToList().First(u => u.Username == username && u.Password == password);
            user.Token = GenerateToken();
            RentItContext.Db.SaveChanges();

            return user;
        }

        /// <summary>
        /// Logs the user in, and assigns a new token.
        /// </summary>
        /// <param name="user">The user to login (with Username and Password)</param>
        /// <returns>User object, containing the user's token</returns>
        public static User Login(User user)
        {
            Contract.Requires<ArgumentNullException>(user.Username != null);
            Contract.Requires<ArgumentNullException>(user.Password != null);

            return Login(user.Username, user.Password);
        }

        /// <summary>
        /// Logs the user out, clearing the session.
        /// </summary>
        /// <param name="user">The user to log out.</param>
        public static void Logout(User user)
        {
            var foundUser = All.First(u => u.ID.Equals(user.ID));
            foundUser.Token = null;
            RentItContext.Db.SaveChanges();
        }

        /// <summary>
        /// Generate a login token.
        /// </summary>
        /// <returns>Login token.</returns>
        public static string GenerateToken()
        {
            Contract.Ensures(Contract.Result<string>() != null);

            string token;

            do
            {
                long i = 1;

                foreach (byte b in Guid.NewGuid().ToByteArray())
                {
                    i *= (b + 1);
                }

                token = string.Format("{0:x}", i - DateTime.Now.Ticks);
            }
            while (All.Any(u => u.Token == token));

            return token;
        }

        /// <summary>
        /// Get user by its token.
        /// </summary>
        /// <param name="token">Token of the user</param>
        /// <returns>The user with the given token</returns>
        [Pure]
        public static User GetByToken(string token)
        {
            Contract.Requires<UserNotFoundException>(token != null);

            if (!All.Any(u => u.Token == token))
            {
                return null;
            }

            return User.All.First(u => u.Token == token);
        }

        #endregion Static methods

        #region Methods

        /// <summary>
        /// Creates a rental entry in the database.
        /// </summary>
        /// <param name="movieEdition">The movie edition to be rented. Only ID is used.</param>
        public void RentMovie(Edition movieEdition)
        {
            Contract.Requires<NotAUserException>(this.Type == UserType.User);

            if (!Movie.All.Any(m => m.Editions.Any(e => e.ID == movieEdition.ID) && m.ReleaseDate != null && m.ReleaseDate <= DateTime.Now))
            {
                throw new NoMovieFoundException("No released movies found with the given ID.");
            }

            RentItContext.Db.Rentals.Add(new Rental { EditionID = movieEdition.ID, UserID = this.ID, Time = DateTime.Now });
            RentItContext.Db.SaveChanges();
        }

        /// <summary>
        /// Updates a user profile.
        /// </summary>
        /// <param name="editedUser">User object with updated info.</param>
        public void Edit(User editedUser)
        {
            Contract.Requires<ArgumentNullException>(editedUser != null);
            Contract.Requires<ArgumentNullException>(editedUser.Email != null);
            Contract.Requires<ArgumentNullException>(editedUser.Password != null);
            Contract.Requires<ArgumentException>(Validator.ValidateEmail(editedUser.Email));

            Contract.Requires<InsufficientRightsException>(this.ID == editedUser.ID);

            this.Email = editedUser.Email;
            this.FullName = editedUser.FullName;
            this.Password = Hash.Sha512(editedUser.Password + Salt);

            RentItContext.Db.SaveChanges();
        }

        /// <summary>
        /// Gets the uses rental history.
        /// </summary>
        /// <returns>The users rental history.</returns>
        public IEnumerable<Rental> GetRentalHistory()
        {
            return this.Rentals;
        }

        /// <summary>
        /// Gets the current rentals of the user.
        /// </summary>
        /// <returns>The current rentals of the user.</returns>
        public IEnumerable<Rental> GetCurrentRentals()
        {
            var daysToRent = int.Parse(ConfigurationManager.AppSettings["RentalDays"]);
            var limitRentalTime = DateTime.Now.AddDays(-daysToRent);

            return Rental.All.Where(r => r.UserID == this.ID & r.Time > limitRentalTime);
        }

        #endregion Methods
    }
}