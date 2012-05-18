// -----------------------------------------------------------------------
// <copyright file="ServiceClients.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace RentItClient.Models
{
    using RentItService;

    /// <summary>
    /// Contains the clients for the models.
    /// </summary>
    public static class ServiceClients
    {
        #region Client fields

        /// <summary>
        /// Client for user management.
        /// </summary>
        public static readonly UserManagementClient UserManagement = new UserManagementClient();

        /// <summary>
        /// Client for content browsing.
        /// </summary>
        public static readonly ContentBrowsingClient ContentBrowsing = new ContentBrowsingClient();

        /// <summary>
        /// Client for content management.
        /// </summary>
        public static readonly ContentManagementClient ContentManagement = new ContentManagementClient();

        /// <summary>
        /// Client for rental management.
        /// </summary>
        public static readonly RentalManagementClient RentalManagement = new RentalManagementClient();
        #endregion
    }
}