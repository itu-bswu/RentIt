// -----------------------------------------------------------------------
// <copyright file="RentalHistoryViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.ViewModels.UserViewModels
{
    using System;
    using System.Collections.Generic;

    using RentItClient.Models;

    /// <summary>
    /// Viewmodel for the Rental History page.
    /// </summary>
    public class RentalHistoryViewModel
    {
        /// <summary>
        /// Gets the rentals of the user that is logged in.
        /// </summary>
        /// <returns>A list of rentals as tuples of string, int and bool types.</returns>
        public static List<Tuple<string, int, bool>> GetRentals()
        {
            var current = UserModel.CurrentRentals();
            var all = UserModel.RentalHistory();

            var result = new List<Tuple<string, int, bool>>();

            foreach (var r in current)
            {
                var mId = r.MovieID;
                var title = GetMovieInformationModel.GetMovieInfo(r.MovieID).Title;
                result.Add(Tuple.Create(title, mId, true));
            }

            foreach (var r in all)
            {
                var mId = r.MovieID;
                var title = GetMovieInformationModel.GetMovieInfo(r.MovieID).Title;
                if (!result.Exists(t => t.Item2 == mId))
                {
                    result.Add(Tuple.Create(title, mId, false));
                }
            }

            return result;
        }
    }
}