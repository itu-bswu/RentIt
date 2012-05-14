// -----------------------------------------------------------------------
// <copyright file="ServiceClients.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace RentItClient.Models
{
    using RentItClient.RentItService;

    /// <summary>
    /// Contains the clients for the models.
    /// </summary>
    public static class ServiceClients
    {
        #region Client fields
        /// <summary>
        /// The client used to access user information on the service.
        /// </summary>
        /// <author>Jakob Melnyk</author>
        public static readonly UserInformationClient Uic = new UserInformationClient();

        /// <summary>
        /// The client used to access movie information on the service.
        /// </summary>
        public static readonly GetMovieDataClient Gmdc = new GetMovieDataClient();

        /// <summary>
        /// The client used to access content features on the service.
        /// </summary>
        public static readonly ContentServiceClient Csc = new ContentServiceClient();

        /// <summary>
        /// The client used to access download features on the service.
        /// </summary>
        public static readonly DownloadServiceClient Dsc = new DownloadServiceClient();

        /// <summary>
        /// The client used to acces upload features on the service.
        /// </summary>
        public static readonly UploadServiceClient Usc = new UploadServiceClient();
        #endregion
    }
}