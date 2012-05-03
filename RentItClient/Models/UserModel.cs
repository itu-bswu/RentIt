// -----------------------------------------------------------------------
// <copyright file="UserModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace RentItClient.Models
{
    using System.Collections.Generic;

    using RentItService;

    /// <summary>
    /// Contains the logic for accessing and editing user information.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    public static class UserModel
    {
        /// <summary>
        /// Rents a movie on the service.
        /// </summary>
        /// <param name="movieId">The id of the movie to rent.</param>
        /// <author>Jakob Melnyk</author>
        public static void RentMovie(int movieId)
        {
            ServiceClients.Uic.RentMovie(AccessModel.LoggedIn.Token, movieId);
        }

        /// <summary>
        /// Edits the users own profile.
        /// </summary>
        /// <param name="user">The user object containing the edited user profile.</param>
        /// <author>Jakob Melnyk</author>
        public static void EditProfile(User user)
        {
            ServiceClients.Uic.EditProfile(AccessModel.LoggedIn.Token, user);
        }

        /// <summary>
        /// Gets the current and previous rentals of the user.
        /// </summary>
        /// <returns>All the current and previous rentals of the user.</returns>
        /// <author>Jakob Melnyk</author>
        public static IEnumerable<Rental> RentalHistory()
        {
            return ServiceClients.Uic.GetRentalHistory(AccessModel.LoggedIn.Token);
        }

        /// <summary>
        /// Gets the current rentals of the user.
        /// </summary>
        /// <returns>The current rentals of the user.</returns>
        /// <author>Jakob Melnyk</author>
        public static IEnumerable<Rental> CurrentRentals()
        {
            return ServiceClients.Uic.GetCurrentRentals(AccessModel.LoggedIn.Token);
        }
    }
}