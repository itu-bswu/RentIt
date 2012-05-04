// -----------------------------------------------------------------------
// <copyright file="RegistrationViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace RentItClient.ViewModels.AdministrationViewModels
{
    using RentItClient.Models;
    using RentItClient.RentItService;

    /// <summary>
    /// Viewmodel for the registration window.
    /// </summary>
    public static class RegistrationViewModel
    {
        /// <summary>
        /// Attempt to sign up a user.
        /// </summary>
        /// <param name="email">The email entered to sign up with.</param>
        /// <param name="fullName">The full name entered to sign up with.</param>
        /// <param name="password">The password entered to sign up with.</param>
        /// <param name="username">The username to sign up with.</param>
        /// <returns>True, if the sign up is successful. False, if it is not.</returns>
        public static bool SignUp(string email, string fullName, string password, string username)
        {
            var user = new User { Email = email, FullName = fullName, Password = password, Username = username };

            return AccessModel.SignUp(user);
        }
    }
}
