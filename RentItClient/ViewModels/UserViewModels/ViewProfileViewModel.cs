// -----------------------------------------------------------------------
// <copyright file="ViewProfileViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.ViewModels.UserViewModels
{
    using Models;
    using Types;

    /// <summary>
    /// Viewmodel for the viewProfile view.
    /// </summary>
    public static class ViewProfileViewModel
    {
        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns>The user that is currently logged in.</returns>
        public static User GetCurrentUserInfo()
        {
            return User.ConvertServiceUser(AccessModel.LoggedIn);
        }
    }
}
