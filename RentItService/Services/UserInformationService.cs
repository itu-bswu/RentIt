//-------------------------------------------------------------------------------------------------
// <copyright file="UserInformationService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using Entities;
    using Enums;
    using Exceptions;
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
            Contract.Requires<ArgumentNullException>(token != null & userObject != null);
            Contract.Requires<ArgumentNullException>(userObject.Username != null);
            Contract.Requires<ArgumentNullException>(userObject.Email != null);
            Contract.Requires<ArgumentNullException>(userObject.Password != null);

            Contract.Requires<InsufficientAccessLevelException>(User.GetByToken(token).ID == userObject.ID);

            return User.EditProfile(token, userObject);
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
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<NotAUserException>(User.GetByToken(token).Type == UserType.User);

            User.RentMovie(token, movieId);
        }

        /// <summary>
        /// Returns a list of all the users.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <author>Jacob Grooss</author>
        /// <returns>The list of users.</returns>
        public IEnumerable<User> GetUsers(string token)
        {
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<UserNotFoundException>(User.GetByToken(token) != null);
            Contract.Requires<InsufficientAccessLevelException>(User.GetByToken(token).Type == UserType.SystemAdmin);

            using (var db = new RentItContext())
            {
                var result = new Collection<User>();

                foreach (var user in db.Users)
                {
                    if (user.Type == UserType.User && !result.Contains(user))
                    {
                        result.Add(user);
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Returns a list of all the content publishers.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <author>Jacob Grooss</author>
        /// <returns>The list of content publishers.</returns>
        public IEnumerable<User> GetContentPublishers(string token)
        {
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<UserNotFoundException>(User.GetByToken(token) != null);
            Contract.Requires<InsufficientAccessLevelException>(User.GetByToken(token).Type == UserType.SystemAdmin);

            using (var db = new RentItContext())
            {
                var result = new Collection<User>();

                foreach (var user in db.Users)
                {
                    if (user.Type == UserType.ContentProvider && !result.Contains(user))
                    {
                        result.Add(user);
                    }
                }

                return result;
            }
        }
    }
}