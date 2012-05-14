// -----------------------------------------------------------------------
// <copyright file="IUserManagement.cs" company="">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.ServiceModel;
    using Entities;

    /// <summary>
    /// Interface for user management.
    /// </summary>
    [ServiceContract]
    public interface IUserManagement
    {
        /// <summary>
        /// Sign up for the service. 
        /// Put in Username, FullName (optional), Email and Password. The rest 
        /// of the fields will be reset and updated with the information provided 
        /// by the service.
        /// </summary>
        /// <param name="user">The user object. Username, Password and Email are required fields. FullName is optional.</param>
        /// <returns>True on success; false otherwise.</returns>
        [OperationContract]
        bool SignUp(ref User user);

        /// <summary>
        /// Log in to the system with the user's username and password. 
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="user">An instance of the User class, containing all information about that user (including a token).</param>
        /// <returns>True on success; false otherwise.</returns>
        [OperationContract]
        bool Login(string username, string password, out User user);

        /// <summary>
        /// Log a user out of the system. Will clear the session token.
        /// </summary>
        /// <param name="token">The user's session token.</param>
        /// <returns>True on success; false otherwise.</returns>
        [OperationContract]
        bool Logout(string token);

        /// <summary>
        /// Edit a user's information. The session token must match the user being 
        /// edited, or the edit will fail. All fields (Email, FullName and Password) 
        /// will be updated, so remember to fill in all the information - also the 
        /// old information, if it shouldn't be changed.
        /// </summary>
        /// <param name="token">The user's session token.</param>
        /// <param name="user">The user to edit. Should contain all new values (Email, FullName and Password).</param>
        /// <returns>True on success; false otherwise.</returns>
        [OperationContract]
        bool EditUser(string token, ref User user);
    }
}
