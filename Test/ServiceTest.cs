//-------------------------------------------------------------------------------------------------
// <copyright file="ServiceTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentIt.Tests.ServiceReference;

    /// <summary>
    /// Base class for all service-level tests. 
    /// Connects to the database specified in Web.config, 
    /// empties it and load the data-set. Then it 
    /// establishes connection to the service.
    /// </summary>
    [TestClass]
    public abstract class ServiceTest : DataTest
    {
        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceTest"/> class. 
        /// Establishes connection to the service.
        /// </summary>
        protected ServiceTest()
        {
            this.UserManagement = new UserManagementClient();
            this.ContentBrowsing = new ContentBrowsingClient();
            this.RentalManagement = new RentalManagementClient();
            this.ContentManagement = new ContentManagementClient();
        }

        #endregion Constructor(s)

        #region Properties

        /// <summary>
        /// Gets or sets the UserManagement service.
        /// </summary>
        protected IUserManagement UserManagement { get; set; }

        /// <summary>
        /// Gets or sets the ContentBrowsing service.
        /// </summary>
        protected IContentBrowsing ContentBrowsing { get; set; }

        /// <summary>
        /// Gets or sets the RentalManagement service.
        /// </summary>
        protected IRentalManagement RentalManagement { get; set; }

        /// <summary>
        /// Gets or sets the ContentManagement service.
        /// </summary>
        protected IContentManagement ContentManagement { get; set; }

        #endregion Properties
    }
}