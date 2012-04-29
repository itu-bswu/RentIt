// -----------------------------------------------------------------------
// <copyright file="AccessModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace RentItClient.Models
{
    using RentItClient.RentItService;

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
        public static bool SignUp(string email, string fullName, string password, string username)
        {
            var user = new User
                {
                    Email = email,
                    FullName = fullName,
                    Password = password,
                    Username = username
                };

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
        /// <param name="user">The user to log out.</param>
        /// <author>Jakob Melnyk</author>
        public static void LogOut(User user)
        {
            ServiceClients.Uic.Logout(user.Token);
            LoggedIn = null;
        }
    }
}