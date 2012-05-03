// -----------------------------------------------------------------------
// <copyright file="MasterViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace RentItClient.ViewModels
{
    using System;
    using System.Collections.Generic;

    using RentItClient.Models;

    /// <summary>
    /// View model containing functionality shared between many pages.
    /// </summary>
    public static class MasterViewModel
    {
        /// <summary>
        /// Logs the user out.
        /// </summary>
        public static void LogOut()
        {
            AccessModel.LogOut();
        }

        /// <summary>
        /// Searches for the specified string.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns>List of tuples containing movie titles and ids.</returns>
        public static List<Tuple<string, int>> Search(string searchString)
        {
            var movies = GetMovieInformationModel.Search(searchString);
            List<Tuple<string, int>> result = new List<Tuple<string, int>>();

            return result;
        }
    }
}
