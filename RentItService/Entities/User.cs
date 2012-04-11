//-------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Entities
{
    using System;
    using System.Collections.Generic;
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

            user.ID = 0;
            user.Type = UserType.User;
            user.Token = string.Empty;
            user.Password = Hash.Sha512(user.Password + Salt);

            using (var db = new RentItContext())
            {
                if (db.Users.Any(u => u.Username == user.Username))
                {
                    throw new UsernameInUseException("Username is already in use!");
                }

                db.Users.Add(user);
                if (db.SaveChanges() > 0)
                {
                    return user;
                }
            }

            return null;
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

            using (var db = new RentItContext())
            {
                password = Hash.Sha512(password + Salt);

                if (!db.Users.Any(u => u.Username == username && u.Password == password))
                {
                    throw new UserNotFoundException("No user with the given login information was found!");
                }

                var user = db.Users.First(u => u.Username == username && u.Password == password);
                user.Token = GenerateToken();
                db.SaveChanges();

                return user;
            }
        }

        /// <summary>
        /// Generate a login token.
        /// </summary>
        /// <returns>Login token.</returns>
        public static string GenerateToken()
        {
            Contract.Ensures(Contract.Result<string>() != null);

            using (var db = new RentItContext())
            {
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
                while (db.Users.Any(u => u.Token == token));

                return token;
            }
        }

        /// <summary>
        /// Get user by its token.
        /// </summary>
        /// <param name="token">Token of the user</param>
        /// <returns>The user with the given token</returns>
        public static User GetByToken(string token)
        {
            Contract.Requires<UserNotFoundException>(token != null);
            Contract.Ensures(Contract.Result<User>() != null);

            using (var db = new RentItContext())
            {
                if (!db.Users.Any(u => u.Token == token))
                {
                    throw new UserNotFoundException("No user with the given token was found!");
                }

                return db.Users.First(u => u.Token == token);
            }
        }

        /// <summary>
        /// Creates a rental entry in the database.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="movieId">The ID of the movie to be rented.</param>
        public static void RentMovie(string token, int movieId)
        {
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<NotAUserException>(GetByToken(token).Type == UserType.User);

            User user = GetByToken(token);

            using (var db = new RentItContext())
            {
                db.Rentals.Add(new Rental { MovieID = movieId, UserID = user.ID, Time = DateTime.Now });
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a user profile.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="userObject">The updated user object.</param>
        /// <returns>The edited user profile.</returns>
        public static User EditProfile(string token, User userObject)
        {
            Contract.Requires<ArgumentNullException>(token != null & userObject != null);
            Contract.Requires<ArgumentNullException>(userObject.Username != null);
            Contract.Requires<ArgumentNullException>(userObject.Email != null);
            Contract.Requires<ArgumentNullException>(userObject.Password != null);

            Contract.Requires<ArgumentException>(userObject.Email != string.Empty & userObject.Email.Contains("@"));
            Contract.Requires<ArgumentException>(userObject.Password != string.Empty);

            Contract.Requires<InsufficientRightsException>(GetByToken(token).ID == userObject.ID);

            using (var db = new RentItContext())
            {
                User user = db.Users.Find(userObject.ID);

                user.Email = userObject.Email;
                user.FullName = userObject.FullName;
                user.Password = Hash.Sha512(userObject.Password + Salt);

                db.SaveChanges();
                user = db.Users.Find(userObject.ID);
                return user;
            }
        }

        /// <summary>
        /// Gets the uses rental history.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>The users rental history.</returns>
        public static IEnumerable<Rental> GetRentalHistory(string token)
        {
            return User.GetByToken(token).Rentals;
        }

        /// <summary>
        /// Gets the current rentals of the user.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>The current rentals of the user.</returns>
        public static IEnumerable<Rental> GetCurrentRentals(string token)
        {
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<ArgumentException>(User.GetByToken(token) != null);

            var user = User.GetByToken(token);
            var limitRentalTime = DateTime.Now.AddDays(-Constants.DaysToRent);

            using (var db = new RentItContext())
            {
                return db.Rentals.Where(r => r.UserID == user.ID & r.Time > limitRentalTime).ToList();
            }
        }

        #endregion Static methods
    }
}