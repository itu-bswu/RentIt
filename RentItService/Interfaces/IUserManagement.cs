// -----------------------------------------------------------------------
// <copyright file="IUserManagement.cs" company="">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using Entities;

    /// <summary>
    /// Interface for user management.
    /// </summary>
    [ServiceContract]
    public interface IUserManagement
    {
        /// <summary>
        /// Sign up for the service
        /// </summary>
        /// <param name="user">The user object. Should have username and password</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool SignUp(ref User user);

        /// <summary>
        /// Log in to the system.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <param name="user">The user's object</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool Login(string username, string password, out User user);

        /// <summary>
        /// Log out of the system.
        /// </summary>
        /// <param name="token">The user token</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool Logout(string token);

        /// <summary>
        /// Edits a user.
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="user">The user to edit. Should at least have ID.</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool EditUser(string token, ref User user);
    }
}
