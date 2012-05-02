// -----------------------------------------------------------------------
// <copyright file="AccessModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace RentItClient.Models
{
    using RentItService;

    /// <summary>
    /// Contains the logic for signing up, logging into and logging out of the RentIt service.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    public static class AccessModel
    {
        /// <summary>
        /// Gets the user that is LoggedIn.
        /// </summary>
        /// <author>Jakob Melnyk</author>
        public static User LoggedIn { get; private set; }

        /// <summary>Attempt to sign up a user on the service.</summary>
        /// <param name="email">The user email.</param>
        /// <param name="fullName">The users full name.</param>
        /// <param name="password">The password the user wants.</param>
        /// <param name="username">The username the user wants.</param>
        /// <returns>True if signup was successful, false if it failed.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool SignUp(User user)
        {
            return ServiceClients.Uic.SignUp(user);
        }

        /// <summary>Logs in the user and returns a User object containing a token that can be used to access the service.</summary>
        /// <param name="username">The users username.</param>
        /// <param name="password">The users password.</param>
        /// <returns>The user object related to the user.</returns>
        /// <author>Jakob Melnyk</author>
        public static User Login(string username, string password)
        {
            LoggedIn = ServiceClients.Uic.LogIn(username, password);
            return LoggedIn;
        }

        /// <summary>Logs the user out of the service by making his/her token invalid.</summary>
        /// <author>Jakob Melnyk</author>
        public static void LogOut()
        {
            ServiceClients.Uic.Logout(LoggedIn.Token);
            LoggedIn = null;
        }
    }
}