// -----------------------------------------------------------------------
// <copyright file="IUserInformation.cs" company="">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.Collections.Generic;
    using System.ServiceModel;

    using RentItService.Entities;

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
        /// <returns>The session token.</returns>
        /// <author>Jakob Melnyk</author>
        [OperationContract]
        string SignUp(User userObject);

        /// <summary>
        /// Logs the user in returning a session token.
        /// </summary>
        /// <param name="userName">The user name used in the signup.</param>
        /// <param name="password">The user password.</param>
        /// <returns>The session token.</returns>
        /// <author>Jakob Melnyk</author>
        [OperationContract]
        string LogIn(string userName, string password);

        /// <summary>
        /// Updates a user profile.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="userObject">The updated user object.</param>
        /// <author>Jakob Melnyk</author>
        [OperationContract]
        void EditProfile(string token, User userObject);

        /// <summary>
        /// Gets all of the previous and current rentals of the user.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>An IEnumerable containing all the users rentals.</returns>
        /// <author>Jakob Melnyk</author>
        [OperationContract]
        IEnumerable<Rental> GetRentalHistory(string token);

        /// <summary>
        /// Gets all of the current rentals.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <returns>An IEnumerable containg the active rentals.</returns>
        /// <author>Jakob Melnyk</author>
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
    }
}
