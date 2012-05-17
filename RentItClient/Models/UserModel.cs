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
        /// <param name="editionId">The id of the movie to rent.</param>
        /// <returns>True if rent was succesful, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool RentEdition(int editionId)
        {
            var edition = new Edition
                        {
                            ID = editionId
                        };

            return ServiceClients.RentalManagement.RentMovie(AccessModel.LoggedIn.Token, edition);
        }

        /// <summary>
        /// Edits the users own profile.
        /// </summary>
        /// <param name="user">The user object containing the edited user profile.</param>
        /// <returns>True if edit was succesful, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool EditProfile(User user)
        {
            var password = user.Password;
            var ret = ServiceClients.UserManagement.EditUser(AccessModel.LoggedIn.Token, ref user);
            user.Password = password;
            AccessModel.UpdateLoggedInUser(user);
            return ret;
        }

        /// <summary>
        /// Gets the current and previous rentals of the user.
        /// </summary>
        /// <param name="rentalHistory">All the current and previous rentals of the user.</param>>
        /// <returns>True if rental information was collected successfully, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool RentalHistory(out IEnumerable<Rental> rentalHistory)
        {
            Rental[] rentals;
            var ret = ServiceClients.RentalManagement.GetRentals(out rentals, AccessModel.LoggedIn.Token, RentalScope.All);
            rentalHistory = rentals;
            return ret;
        }

        /// <summary>
        /// Gets the current rentals of the user.
        /// </summary>
        /// <param name="currentRentals">The current rentals of the user.</param>
        /// <returns>True if rental information was collected successfully, false if not.</returns>
        /// <author>Jakob Melnyk</author>
        public static bool CurrentRentals(out IEnumerable<Rental> currentRentals)
        {
            Rental[] rentals;
            var ret = ServiceClients.RentalManagement.GetRentals(out rentals, AccessModel.LoggedIn.Token, RentalScope.Current);
            currentRentals = rentals;
            return ret;
        }
    }
}