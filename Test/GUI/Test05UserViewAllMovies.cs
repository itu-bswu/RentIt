// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Test05UserViewAllMovies.cs" company="RentIt">
//   Copyright (c) RentIt. All rights reserved.
// </copyright>
// <summary>
//   Test 5 - User, view list of all offered movies
//   1. Login as the user "Smith"
//   2. Navigate to the View Movie List Page
//   3. Sort the movies by Newest and All
//   4. Close the window
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.GUI
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test 5 - User, view list of all offered movies
    /// 1. Login as the user "Smith"
    /// 2. Navigate to the View Movie List Page
    /// 3. Sort the movies by Newest and All
    /// 4. Close the window
    /// </summary>
    [CodedUITest]
    public class Test05UserViewAllMovies
    {
        #region Fields and Properties

        /// <summary>
        /// The Test Context
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// The UIMap that contains the needed methods for the test to r
        /// </summary>
        private UIMap map;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }

            set
            {
                this.testContextInstance = value;
            }
        }

        /// <summary>
        /// Gets the UIMap that contains the methods
        /// that the test calls.
        /// </summary>
        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }
        #endregion

        /// <summary>
        /// The Test Method for GUI Test 5. 
        /// See the summary for the class for the test steps.
        /// </summary>
        [TestMethod]
        public void GuiTest05UserViewAllMovies()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
            var projectPath =
                Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"Client\bin\Debug\RentItClient.exe");

            System.Diagnostics.Process.Start(projectPath);

            this.UIMap.Test05UserViewAllMovies();

            // Reset database after method calls, because inheriting from DataTest bugs out the UI test for some reason.
            var dt = new DataTest();
            dt.TestInitialize();
            dt.TestInit();
        }
    }
}
