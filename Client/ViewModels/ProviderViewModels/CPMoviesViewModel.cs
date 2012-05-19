// -----------------------------------------------------------------------
// <copyright file="CPMoviesViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.ViewModels.ProviderViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Types;

    /// <summary>
    /// Viewmodel for the CPYourMoviesPage page.
    /// </summary>
    public static class CPMoviesViewModel
    {
        /// <summary>
        /// Gets all the movies registered by the content provider that is currently logged in.
        /// </summary>
        /// <returns>Gets the movies of the content provider.</returns>
        public static IEnumerable<Tuple<string, Movie>> GetMovies()
        {
            IEnumerable<RentItService.Movie> allMovies;
            MovieInformationModel.AllMovies(out allMovies);
            return allMovies.Select(m => Tuple.Create(m.Title, Movie.ConvertServiceMovie(m))).ToList();
        }
    }
}
