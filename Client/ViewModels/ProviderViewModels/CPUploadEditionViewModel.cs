// -----------------------------------------------------------------------
// <copyright file="CPUploadEditionViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItClient.ViewModels.ProviderViewModels
{
    using System.IO;
    using Models;
    using Types;

    /// <summary>
    /// View model for the 
    /// </summary>
    public static class CPUploadEditionViewModel
    {
        /// <summary>
        /// </summary>
        /// <param name="m">
        /// The m.
        /// </param>
        /// <param name="editionName">
        /// The edition Name.
        /// </param>
        /// <param name="fi">
        /// The fi.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool UploadEdition(Movie m, string editionName, FileInfo fi)
        {
            return AdministrationModel.UploadEdition(editionName, m.ID, fi);
        }
    }
}
