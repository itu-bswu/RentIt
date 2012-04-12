﻿// -----------------------------------------------------------------------
// <copyright file="IUserInformation.cs" company="">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using Entities;

    /// <summary>
    /// Service contract for user information.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    [ServiceContract]
    public interface IUserInformation
    {
        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="userObject">The user object containg the user information.</param>
        /// <returns>True for success; false otherwise.</returns>
        /// <author>Niklas Hansen</author>
        [OperationContract]
        bool SignUp(User userObject);

        /// <summary>
        /// Logs the user in returning user information (and a session token).
        /// </summary>
        /// <param name="userName">The user name used in the signup.</param>
        /// <param name="password">The user password.</param>
        /// <returns>The session token.</returns>
        /// <author>Niklas Hansen</author>
        [OperationContract]
        User LogIn(string userName, string password);

        /// <summary>
        /// Logs the user out, clearing the session.
        /// </summary>
        /// <param name="token">The session token.</param>
        [OperationContract]
        void Logout(string token);

        /// <summary>
        /// Updates a user profile.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="userObject">The updated user object.</param>
        /// <returns>The resulting user object.</returns>
        /// <author>Jakob Melnyk</author>
        [OperationContract]
        User EditProfile(string token, User userObject);

        /// <summary>
        /// Gets all of the previous and current rentals of the user.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>An IEnumerable containing all the users rentals.</returns>
        /// <author>TBD</author>
        [OperationContract]
        IEnumerable<Rental> GetRentalHistory(string token);

        /// <summary>
        /// Gets all of the current rentals.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>An IEnumerable containg the active rentals.</returns>
        /// <author>TBD</author>
        [OperationContract]
        IEnumerable<Rental> GetCurrentRentals(string token);

        /// <summary>
        /// Creates a rental entry in the database.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="movieId">The ID of the movie to be rented.</param>
        /// <author>Jakob Melnyk</author>
        [OperationContract]
        void RentMovie(string token, int movieId);

        /// <summary>
        /// Returns a list of all the users.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <author>Jacob Grooss</author>
        /// <returns>The list of users.</returns>
        [OperationContract]
        IEnumerable<User> GetUsers(string token);

        /// <summary>
        /// Returns a list of all the content publishers.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <author>Jacob Grooss</author>
        /// <returns>The list of content publishers.</returns>
        [OperationContract]
        IEnumerable<User> GetContentPublishers(string token);
    }
}
