// -----------------------------------------------------------------------
// <copyright file="MostRentedViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.ViewModels.UserViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RentItClient.Models;
    using RentItClient.RentItService;

    /// <summary>
    /// Viewmodel for the most rented user page.
    /// </summary>
    public static class MostRentedViewModel
    {
        /// <summary>
        /// Gets the most rented movies.
        /// </summary>
        /// <returns>A list containing tuples of matching movie titles/movieIds</returns>
        public static List<Tuple<string, int>> MostRentedMovies()
        {
            var movies = GetMovieInformationModel.MostDownloaded();
            var movieTuples = movies.Select(m => Tuple.Create(m.Title, m.ID)).ToList();
            return movieTuples;
        }
    }
}
