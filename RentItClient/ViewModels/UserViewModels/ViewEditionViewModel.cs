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
        public static void RentEdition(int editionId)
        {
            if (!UserModel.RentEdition(editionId))
            {
                MasterViewModel.AuthenticationError();
            }
        }
    }
}
