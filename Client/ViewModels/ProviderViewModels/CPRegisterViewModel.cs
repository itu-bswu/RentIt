// -----------------------------------------------------------------------
// <copyright file="CPRegisterViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItClient.ViewModels.ProviderViewModels
{
    using Models;
    using Types;
    /// <summary>
    /// Viewmodel for the CPRegisterMovie page.
    /// </summary>
    public static class CPRegisterViewModel
    {
        /// <summary>
        /// Registers a movie with the service.
        /// </summary>
        /// <param name="movie">The movie to register.</param>
        /// <returns>True if registration was successful, false if not.</returns>
        public static bool RegisterMovie(ref Movie movie)
        {
            var serviceMovie = Movie.ConvertClientMovie(movie);

            var success = AdministrationModel.RegisterMovie(ref serviceMovie);

            movie = Movie.ConvertServiceMovie(serviceMovie);

            return success;
        }
    }
}
