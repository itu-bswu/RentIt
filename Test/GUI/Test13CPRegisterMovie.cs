// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Test13CPRegisterMovie.cs" company="RentIt">
//   Copyright (c) RentIt. All rights reserved.
// </copyright>
// <summary>
//   Test 13 - CP, register movie
//   1. Login as test content provider
//   2. Click the "Register movie" button
//   3. Make a movie with the name "Bleach", release date "12-05-2012", description "Action!" and the genres "Action"
//   4. Click the "Register movie" button
//   5. Click "No" when asked if one wants to upload an edition right away
//   6. Find the movie in the list of Your movies
//   7. Assert that it's the correct movie
//   8. Close the window
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.GUI
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test 13 - CP, register movie
    /// 1. Login as test content provider
    /// 2. Click the "Register movie" button
    /// 3. Make a movie with the name "Bleach", release date "12-05-2012", description "Action!" and the genres "Action"
    /// 4. Click the "Register movie" button
    /// 5. Click "No" when asked if one wants to upload an edition right away
    /// 6. Find the movie in the list of Your movies
    /// 7. Assert that it's the correct movie
    /// 8. Close the window
    /// </summary>
    [CodedUITest]
    public class Test13CPRegisterMovie
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
        /// The Test Method for GUI Test 13. 
        /// See the summary for the class for the test steps.
        /// </summary>
        [TestMethod]
        public void GuiTest13CPRegisterMovie()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
            var projectPath =
                Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"Client\bin\Debug\RentItClient.exe");

            System.Diagnostics.Process.Start(projectPath);

            this.UIMap.Test13CPRegisterMovie();

            // Reset database after method calls, because inheriting from DataTest bugs out the UI test for some reason.
            var dt = new DataTest();
            dt.TestInitialize();
            dt.TestInit();
        }
    }
}
