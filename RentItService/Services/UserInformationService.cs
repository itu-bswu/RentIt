﻿
namespace RentItService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using RentItService.Entities;
    using RentItService.Interfaces;

    /// <summary>
    /// Service for accessing user information.
    /// </summary>
    public partial class Service : IUserInformation
    {
        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="userObject">The user object containg the user information.</param>
        /// <returns>The session token.</returns>
        /// <exception cref="NotImplementedException">Not Yet Implemented</exception>
        public bool SignUp(User userObject)
        {
            Contract.Requires(userObject != null);
            Contract.Requires(userObject.Username != null);
            Contract.Requires(userObject.Email != null);
            Contract.Requires(userObject.Password != null);

            return (User.SignUp(userObject) != null);
        }

        /// <summary>
        /// Logs the user in returning a session token.
        /// </summary>
        /// <param name="userName">The user name used in the signup.</param>
        /// <param name="password">The user password.</param>
        /// <returns>The session token.</returns>
        /// <exception cref="NotImplementedException">Not Yet Implemented</exception>
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
        /// <exception cref="NotImplementedException">Not Yet Implemented</exception>
        public void EditProfile(string token, User userObject)
        {
            // TODO: Implement EditProfile
            throw new NotImplementedException();
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
        /// <exception cref="NotImplementedException">Not Yet Implemented</exception>
        public void RentMovie(string token, int movieId)
        {
            // TODO: Implement RentMovie
            throw new NotImplementedException();
        }
    }
}