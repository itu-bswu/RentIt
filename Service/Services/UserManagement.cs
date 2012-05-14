//-------------------------------------------------------------------------------------------------
// <copyright file="UserManagement.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using Entities;
    using Interfaces;
    using Tools;

    /// <summary>
    /// Service for accessing user information.
    /// </summary>
    public partial class Service : IUserManagement
    {
        /// <summary>
        /// Sign up for the service. 
        /// Put in Username, FullName (optional), Email and Password. The rest 
        /// of the fields will be reset and updated with the information provided 
        /// by the service.
        /// </summary>
        /// <param name="user">The user object. Username, Password and Email are required fields. FullName is optional.</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool SignUp(ref User user)
        {
            if (user == null || 
                user.Username == null || user.Password == null || 
                user.Email == null || !Validator.ValidateEmail(user.Email))
            {
                return false;
            }

            user = User.SignUp(user);
            return true;
        }

        /// <summary>
        /// Log in to the system with the user's username and password. 
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="user">An instance of the User class, containing all information about that user (including a token).</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool Login(string username, string password, out User user)
        {
            if (username == null || password == null)
            {
                user = null;
                return false;
            }

            user = User.Login(username, password);
            return true;
        }

        /// <summary>
        /// Log a user out of the system. Will clear the session token.
        /// </summary>
        /// <param name="token">The user's session token.</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool Logout(string token)
        {
            if (token == null)
            {
                return false;
            }

            var user = User.GetByToken(token);
            if (user == null)
            {
                return false;
            }

            User.Logout(user);
            return true;
        }

        /// <summary>
        /// Edit a user's information. The session token must match the user being 
        /// edited, or the edit will fail. All fields (Email, FullName and Password) 
        /// will be updated, so remember to fill in all the information - also the 
        /// old information, if it shouldn't be changed.
        /// </summary>
        /// <param name="token">The user's session token.</param>
        /// <param name="user">The user to edit. Should contain all new values (Email, FullName and Password).</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool EditUser(string token, ref User user)
        {
            if (token == null || user == null)
            {
                return false;
            }

            var editingUser = User.GetByToken(token);
            if (editingUser == null ||
                user.Username == null || 
                user.Password == null || 
                !Validator.ValidateEmail(user.Email))
            {
                return false;
            }

            user = User.Edit(editingUser, user);
            return true;
        }
    }
}