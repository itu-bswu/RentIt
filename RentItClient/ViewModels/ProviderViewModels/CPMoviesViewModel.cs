// -----------------------------------------------------------------------
// <copyright file="CPMoviesViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItClient.ViewModels.ProviderViewModels
{
    using System;
    using System.Collections.Generic;
    using Types;

    /// <summary>
    /// Viewmodel for the CPYourMoviesPage page.
    /// </summary>
    public static class CPMoviesViewModel
    {
        public static IEnumerable<Tuple<string, Movie>> GetMovies()
        {
            var returnValue = new List<Tuple<string, Movie>>();
            return returnValue; // TODO: Actually implement
        }
    }
}
