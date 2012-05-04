// -----------------------------------------------------------------------
// <copyright file="DownloadMovieViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.ViewModels.UserViewModels
{
    using System.IO;

    using RentItClient.Models;
    using RentItClient.Types;

    /// <summary>
    /// Viewmodel for the DownloadMovie page.
    /// </summary>
    public class DownloadMovieViewModel
    {
        /// <summary>
        /// Gets information about a movie from its ID.
        /// </summary>
        /// <param name="id">The movie to get information about.</param>
        /// <returns>The requested movie.</returns>
        public static Movie GetMovieInfo(int id)
        {
            return Movie.ConvertServiceMovie(GetMovieInformationModel.GetMovieInfo(id));
        }

        /// <summary>
        /// Downloads a movie.
        /// </summary>
        /// <param name="id">The id of the movie to download.</param>
        /// <param name="folder">The folder to save the movie in.</param>
        public static void DownloadMovie(int id, string folder)
        {
            AdministrationModel.DownloadFile(id, folder);
        }
    }
}
