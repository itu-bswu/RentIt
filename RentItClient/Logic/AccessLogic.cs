// -----------------------------------------------------------------------
// <copyright file="AccessLogic.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace RentItClient.Logic
{
    using RentItService;

    /// <summary>
    /// Contains the logic for signing up, logging into and logging out of the RentIt service.
    /// </summary>
    public class AccessLogic
    {
        /// <summary>
        /// The service connection used to access user information.
        /// </summary>
        private readonly UserInformationClient uic;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessLogic"/> class.
        /// </summary>
        public AccessLogic()
        {
            this.uic = new UserInformationClient();
        }

        /// <summary>Attempt to sign up a user on the service.</summary>
        /// <param name="email">The user email.</param>
        /// <param name="fullName">The users full name.</param>
        /// <param name="password">The password the user wants.</param>
        /// <param name="username">The username the user wants.</param>
        /// <returns>True if signup was successful, false if it failed.</returns>
        public bool SignUp(string email, string fullName, string password, string username)
        {
            var user = new User
                {
                    Email = email,
                    FullName = fullName,
                    Password = password,
                    Username = username
                };

            return this.uic.SignUp(user);
        }

        /// <summary>Logs in the user and returns a User object containing a token that can be used to access the service.</summary>
        /// <param name="username">The users username.</param>
        /// <param name="password">The users password.</param>
        /// <returns>The user object related to the user.</returns>
        public User Login(string username, string password)
        {
            return this.uic.LogIn(username, password);
        }

        /// <summary>Logs the user out of the service by making his/her token invalid.</summary>
        /// <param name="user">The user to log out.</param>
        public void LogOut(User user)
        {
            this.uic.Logout(user.Token);
        }
    }
}