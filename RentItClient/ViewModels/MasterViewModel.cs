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
        public static List<Tuple<string, int, bool>> Search(string searchString)
        {
            var current = UserModel.CurrentRentals().ToList();
            var search = GetMovieInformationModel.Search(searchString).ToList();

            var result = new List<Tuple<string, int, bool>>();

            foreach (var s in search)
            {
                var mId = s.ID;
                var title = GetMovieInformationModel.GetMovieInfo(s.ID).Title;
                result.Add(
                    current.All(a => a.MovieID != mId)
                        ? Tuple.Create(title, mId, false)
                        : Tuple.Create(title, mId, true));
            }

            return result;
        }
    }
}
