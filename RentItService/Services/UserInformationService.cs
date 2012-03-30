//-------------------------------------------------------------------------------------------------
// <copyright file="UserInformationService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Entities;
    using Interfaces;

    /// <summary>
    /// Service for accessing user information.
    /// </summary>
    public partial class Service : IUserInformation
    {
        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="userObject">The user object containg the user information.</param>
        /// <returns>True for success; false otherwise.</returns>
        public bool SignUp(User userObject)
        {
            Contract.Requires(userObject != null);
            Contract.Requires(userObject.Username != null);
            Contract.Requires(userObject.Email != null);
            Contract.Requires(userObject.Password != null);

            return (User.SignUp(userObject) != null);
        }

        /// <summary>
        /// Logs the user in returning user information (and a session token).
        /// </summary>
        /// <param name="userName">The user name used in the signup.</param>
        /// <param name="password">The user password.</param>
        /// <returns>The user information.</returns>
        public User LogIn(string userName, string password)
        {
            Contract.Requires(userName != null);
            Contract.Requires(password != null);

            return User.Login(userName, password);
        }

        /// <summary>
        /// Updates a user profile.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="userObject">The updated user object.</param>
        /// <returns>The edited user profile.</returns>
        public User EditProfile(string token, User userObject)
        {
            User u = User.GetByToken(token);
            if (u.Type != UserType.User & u.Type != UserType.SystemAdmin)
            {
                throw new Exception(); // TODO: Throw better exception.
            }

            using (var db = new RentItContext())
            {

                User user = db.Users.Find(userObject.ID);
                if (u.ID != user.ID)
                {
                    throw new Exception(); // TODO: Throw better exception
                }
                user.Email = userObject.Email;
                user.FullName = userObject.FullName;
                user.Password = userObject.Password;
                db.SaveChanges();
                user = db.Users.Find(userObject.ID);
                return user;
            }
        }

        /// <summary>
        /// Gets all of the previous and current rentals of the user.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>An IEnumerable containing all the users rentals.</returns>
        /// <exception cref="NotImplementedException">Not Yet Implemented</exception>
        public IEnumerable<Rental> GetRentalHistory(string token)
        {
            // TODO: Implement GetRentalHistory
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all of the current rentals.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>An IEnumerable containg the active rentals.</returns>
        /// <exception cref="NotImplementedException">Not Yet Implemented</exception>
        public IEnumerable<Rental> GetCurrentRentals(string token)
        {
            // TODO: Implement GetCurrentRentals
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a rental entry in the database.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="movieId">The ID of the movie to be rented.</param>
        public void RentMovie(string token, int movieId)
        {
            User user = User.GetByToken(token);
            if (user.Type != UserType.User)
            {
                throw new Exception(); // TODO: Throw better exception.
            }

            using (var db = new RentItContext())
            {
                db.Rentals.Add(new Rental()
                    {
                        MovieID = movieId,
                        UserID = user.ID,
                        Time = DateTime.Now
                    });
                db.SaveChanges();
            }
        }
    }
}