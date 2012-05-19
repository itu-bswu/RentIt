// -----------------------------------------------------------------------
// <copyright file="ViewEditionViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItClient.ViewModels.UserViewModels
{
    using Models;

    /// <summary>
    /// Viewmodel for the ViewEdition page.
    /// </summary>
    public static class ViewEditionViewModel
    {
        /// <summary>
        /// Rents an edition of a movie.
        /// </summary>
        /// <param name="editionId">The edition to rent.</param>
        public static void RentEdition(int editionId)
        {
            if (!UserModel.RentEdition(editionId))
            {
                MasterViewModel.AuthenticationError();
            }
        }
    }
}
