// -----------------------------------------------------------------------
// <copyright file="EditProfileViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.ViewModels.UserViewModels
{
    using Models;
    using RentItService;

    /// <summary>
    /// Viewmodel for the EditProfile page.
    /// </summary>
    public class EditProfileViewModel
    {
        /// <summary>
        /// Edits user information.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if edit was successful, false if not.</returns>
        public static bool EditUserProfile(string email, string fullName, string password)
        {
            var user = new User { Email = email, FullName = fullName, Password = password, Username = AccessModel.LoggedIn.Username };
            return UserModel.EditProfile(user);
        }
    }
}
