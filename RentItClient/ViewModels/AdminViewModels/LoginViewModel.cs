// -----------------------------------------------------------------------
// <copyright file="LoginViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.ViewModels.UserViewModels
{
    using System;
    using Models;

    /// <summary>
    /// Viewmodel for the login page.
    /// </summary>
    public static class LoginViewModel
    {
        /// <summary>
        /// Gets the type of the user that is logged in.
        /// </summary>
        public static Types.UserType LoggedInUser
        {
            get
            {
                switch (AccessModel.LoggedIn.Type)
                {
                    case RentItService.UserType.User:
                        return Types.UserType.User;

                    case RentItService.UserType.ContentProvider:
                        return Types.UserType.ContentProvider;

                    case RentItService.UserType.SystemAdmin:
                        return Types.UserType.Admin;

                    default:
                        throw new NotImplementedException("User is of a type that is not recognised.");
                }
            }
        }

        /// <summary>
        /// Logs the user in.
        /// </summary>
        /// <param name="username">The user to log in.</param>
        /// <param name="password">The users password.</param>
        public static void Login(string username, string password)
        {
            AccessModel.Login(username, password);
        }
    }
}
