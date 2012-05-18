// -----------------------------------------------------------------------
// <copyright file="MasterViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using Models;
    using RentItService;

    /// <summary>
    /// View model containing functionality shared between many pages.
    /// </summary>
    public static class MasterViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the closing message should be skipped.
        /// </summary>
        public static bool SkipClosingMessage { get; set; }

        /// <summary>
        /// Logs the user out.
        /// </summary>
        /// <returns>True if logout was successful, false if not..</returns>
        public static bool LogOut()
        {
            return AccessModel.LogOut();
        }

        /// <summary>
        /// Searches for the specified string.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns>List of tuples containing movie titles and ids.</returns>
        public static List<Tuple<string, int>> Search(string searchString)
        {
            var returnValue = new List<Tuple<string, int>>();
            IEnumerable<Movie> searchResult;

            var success = MovieInformationModel.Search(out searchResult, searchString);

            if (success)
            {
                returnValue.AddRange(searchResult.Select(s => Tuple.Create(s.Title, s.ID)));

                return returnValue;
            }

            AuthenticationError();
            return null;
        }

        /// <summary>
        /// Checks if a movie is currently rented.
        /// </summary>
        /// <param name="editionId">The movie id to check.</param>
        /// <returns>True if movie is currently rented, false if not.</returns>
        public static bool IsCurrentRental(int editionId)
        {
            IEnumerable<Rental> currentRentals;

            if (UserModel.CurrentRentals(out currentRentals))
            {
                return currentRentals.Any(r => r.EditionID == editionId);
            }

            AuthenticationError();
            return false;
        }

        /// <summary>
        /// Shows a message box with an authentication error message then closes the application.
        /// </summary>
        public static void AuthenticationError()
        {
            MessageBox.Show("An authentication error occured. The client will have to close.");
            SkipClosingMessage = false;
            Application.Current.MainWindow.Close();
        }
    }
}