// -----------------------------------------------------------------------
// <copyright file="ViewMovieViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.ViewModels.UserViewModels
{
    using Models;
    using Types;

    /// <summary>
    /// Viewmodel for the ViewMovie page.
    /// </summary>
    public static class ViewMovieViewModel
    {
        /// <summary>
        /// Gets information about a movie from its ID.
        /// </summary>
        /// <param name="id">The movie to get information about.</param>
        /// <returns>The requested movie.</returns>
        public static Movie GetMovieInfo(int id)
        {
            RentItService.Movie m;
            var success = MovieInformationModel.GetMovieInfo(id, out m);

            if (success)
            {
                return Movie.ConvertServiceMovie(m);
            }

            MasterViewModel.AuthenticationError();
            return null;
        }
    }
}
