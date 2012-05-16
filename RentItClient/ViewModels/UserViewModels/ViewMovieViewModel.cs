// -----------------------------------------------------------------------
// <copyright file="ViewMovieViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.ViewModels.UserViewModels
{
    using RentItClient.Models;
    using RentItClient.Types;

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
            return Movie.ConvertServiceMovie(MovieInformationModel.GetMovieInfo(id));
        }

        /// <summary>
        /// Rents a movie for the user that is logged in.
        /// </summary>
        /// <param name="id">The id of the movie to rent.</param>
        public static void RentMovie(int id)
        {
            UserModel.RentMovie(id);
        }
    }
}
