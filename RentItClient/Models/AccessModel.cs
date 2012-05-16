// -----------------------------------------------------------------------
// <copyright file="AccessModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

using System.Diagnostics.Contracts;

namespace RentItClient.Models
{
    using System.Windows;
    using RentItService;

    /// <summary>
    /// Contains the logic for signing up, logging into and logging out of the RentIt service.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    public static class AccessModel
    {
        /// <summary>
        /// The user that is logged in.
        /// </summary>
        private static User loggedIn;

        /// <summary>
        /// Gets the user that is LoggedIn.
        /// </summary>
        /// <author>Jakob Melnyk</author>
        public static User LoggedIn
        {
            get
            {
                if (loggedIn == null)
                {
                    MessageBox.Show(
                        "An error occured when trying to access the service. You will now be taken to the login screen. " +
                        "\n Any unsaved changes you have made will be lost.");
                }

                return loggedIn;
            }

            private set
            {
                loggedIn = value;
            }
        }

        /// <summary>
        /// Attempt to sign up a user on the service.
        /// </summary>
        /// <param name="user">The user to sign up.</param>
        /// <returns>True if signup was successful, false if it failed.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool SignUp(User user)
        {
            var ret = ServiceClients.UserManagement.SignUp(ref user);
            LoggedIn = user;
            return ret;
        }

        /// <summary>
        /// Logs in the user and returns a User object containing a token that can be used to access the service.
        /// </summary>
        /// <param name="username">The users username.</param>
        /// <param name="password">The users password.</param>
        /// <returns>True if login successful, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool Login(string username, string password)
        {
            var ret = ServiceClients.UserManagement.Login(out loggedIn, username, password);
            LoggedIn.Password = password;
            return ret;
        }

        /// <summary>
        /// Logs the user out of the service by making his/her token invalid.
        /// </summary>
        /// <returns>True if logout successful, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool LogOut()
        {
            var ret = ServiceClients.UserManagement.Logout(LoggedIn.Token);
            LoggedIn = null;
            return ret;
        }

        /// <summary>
        /// Updates the user that is currently logged in.
        /// </summary>
        /// <param name="user">The updated user info.</param>
        public static void UpdateLoggedInUser(User user)
        {
            Contract.Requires(user != null);
            Contract.Requires(user.Username != null &&
                              user.Token != null &&
                              user.Password != null);

            LoggedIn = user;
        }
    }
}