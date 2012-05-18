// -----------------------------------------------------------------------
// <copyright file="DownloadEditionViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.ViewModels.UserViewModels
{
    using Models;

    /// <summary>
    /// Viewmodel for the DownloadEdition page.
    /// </summary>
    public class DownloadEditionViewModel
    {
        /// <summary>
        /// Downloads a movie.
        /// </summary>
        /// <param name="id">The id of the edition to download.</param>
        /// <param name="folder">The folder to save the movie in.</param>
        public static void DownloadFile(int id, string folder)
        {
            AdministrationModel.DownloadFile(id, folder);
        }
    }
}
