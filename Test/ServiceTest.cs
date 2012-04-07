//-------------------------------------------------------------------------------------------------
// <copyright file="ServiceTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ServiceReference;

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
            this.UserInformation = new UserInformationClient();
            this.GetMovieData = new GetMovieDataClient();
            this.ContentService = new ContentServiceClient();
            this.UploadService = new UploadServiceClient();
            this.DownloadService = new DownloadServiceClient();
            System.Console.WriteLine("ServiceTest");
        }

        #endregion Constructor(s)

        #region Properties

        /// <summary>
        /// Gets or sets the UserInformation service.
        /// </summary>
        protected IUserInformation UserInformation { get; set; }

        /// <summary>
        /// Gets or sets the GetMovieData service.
        /// </summary>
        protected IGetMovieData GetMovieData { get; set; }

        /// <summary>
        /// Gets or sets the Content service.
        /// </summary>
        protected IContentService ContentService { get; set; }

        /// <summary>
        /// Gets or sets the Upload service.
        /// </summary>
        protected IUploadService UploadService { get; set; }

        /// <summary>
        /// Gets or sets the Download service.
        /// </summary>
        protected IDownloadService DownloadService { get; set; }

        #endregion Properties
    }
}
