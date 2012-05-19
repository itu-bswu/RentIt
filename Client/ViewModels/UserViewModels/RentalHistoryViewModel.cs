// -----------------------------------------------------------------------
// <copyright file="RentalHistoryViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace RentItClient.ViewModels.UserViewModels
{
    using System;
    using System.Collections.Generic;

    using Models;
    using Types;

    /// <summary>
    /// Viewmodel for the Rental History page.
    /// </summary>
    public class RentalHistoryViewModel
    {
        /// <summary>
        /// Gets the rentals of the user that is logged in.
        /// </summary>
        /// <returns>A list of rentals as tuples of string, int and bool types.</returns>
        public static List<Tuple<string, int, Movie>> GetRentals()
        {
            IEnumerable<RentItService.Rental> res;
            var result = new List<Tuple<string, int, Movie>>();

            var success = UserModel.RentalHistory(out res);
            if (success)
            {
                foreach (var r in res)
                {
                    var e = r.Edition;
                    RentItService.Movie m;
                    if (!MovieInformationModel.GetMovieInfo(e.MovieID, out m))
                    {
                        MasterViewModel.AuthenticationError();
                    }

                    result.Add(Tuple.Create(m.Title + " - " + e.Name, e.ID, Movie.ConvertServiceMovie(m)));
                }

                return result;
            }

            MasterViewModel.AuthenticationError();
            return null;
        }
    }
}