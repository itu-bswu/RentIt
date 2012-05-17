// -----------------------------------------------------------------------
// <copyright file="CPEditMovieViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------



namespace RentItClient.ViewModels.ProviderViewModels
{
    using Models;
    using Types;

    /// <summary>
    /// Viewmodel for the EditMovie page.
    /// </summary>
    public class CPEditMovieViewModel
    {
        /// <summary>
        /// Edits a movie on the service.
        /// </summary>
        /// <param name="m">The edited movie.</param>
        /// <returns>True if edit was successful, false if not.</returns>
        public static bool EditMovie(Movie m)
        {
            var movie = Movie.ConvertClientMovie(m);
            return AdministrationModel.EditMovie(movie, out movie);
        }
    }
}
