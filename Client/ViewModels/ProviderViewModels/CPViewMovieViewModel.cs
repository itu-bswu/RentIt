// -----------------------------------------------------------------------
// <copyright file="CPViewMovieViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.ViewModels.ProviderViewModels
{
    using System.Windows;
    using Models;
    using Types;

    /// <summary>
    /// Viewmodel for the CPViewMovie page.
    /// </summary>
    public class CPViewMovieViewModel
    {
        /// <summary>
        /// Gets information about a movie from its ID.
        /// </summary>
        /// <param name="id">The id of the movie to get.</param>
        /// <returns>The requested movie.</returns>
        public static Movie GetMovieInformation(int id)
        {
            RentItService.Movie m;

            if (MovieInformationModel.GetMovieInfo(id, out m))
            {
                return Movie.ConvertServiceMovie(m);
            }

            MasterViewModel.AuthenticationError();
            return null;
        }

        /// <summary>
        /// Deletes a movie from the service.
        /// </summary>
        /// <param name="m">The movie to delete.</param>
        /// <returns>True if deleteion was successful, false if not.</returns>
        public static bool DeleteMovie(Movie m)
        {
            return AdministrationModel.DeleteMovie(Movie.ConvertClientMovie(m));
        }

        /// <summary>
        /// Deletes an edition from the service.
        /// </summary>
        /// <param name="editionId">The edition to delete.</param>
        public static void DeleteEdition(int editionId)
        {
            var e = new RentItService.Edition
                        {
                            ID = editionId
                        };

            if (AdministrationModel.DeleteEdition(e))
            {
                MessageBox.Show("Deletion was successful.");
            }

            MasterViewModel.AuthenticationError();
        }
    }
}
