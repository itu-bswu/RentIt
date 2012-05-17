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
    using Models;

    /// <summary>
    /// Viewmodel for the CPYourMoviesPage page.
    /// </summary>
    public static class CPMoviesViewModel
    {
        public static IEnumerable<Tuple<string, Movie>> GetMovies()
        {
            var returnValue = new List<Tuple<string, Movie>>();
            IEnumerable<RentItService.Movie> allMovies;
            MovieInformationModel.AllMovies(out allMovies);
            foreach (var m in allMovies)
            {
                returnValue.Add(Tuple.Create(m.Title, Movie.ConvertServiceMovie(m)));
            }
            return returnValue; // TODO: Actually implement
        }
    }
}
