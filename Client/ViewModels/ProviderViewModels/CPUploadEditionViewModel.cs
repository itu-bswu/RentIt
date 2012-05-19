// -----------------------------------------------------------------------
// <copyright file="CPUploadEditionViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.ViewModels.ProviderViewModels
{
    using System.IO;
    using Models;
    using Types;

    /// <summary>
    /// View model for the UploadEdition page.
    /// </summary>
    public static class CPUploadEditionViewModel
    {
        /// <summary> 
        /// Uploads an edition of a movie to the service.
        /// </summary>
        /// <param name="m">The movie the edition is attached to..</param>
        /// <param name="editionName">The edition name.</param>
        /// <param name="fi">The file to upload.</param>
        /// <returns>True if upload was successful, false if not.</returns>
        public static bool UploadEdition(Movie m, string editionName, FileInfo fi)
        {
            return AdministrationModel.UploadEdition(editionName, m.ID, fi);
        }
    }
}
