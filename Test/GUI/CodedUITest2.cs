﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace RentIt.Tests.GUI
{
    using System.IO;

    /// <summary>
    /// Test 4 - User, logout as user
    /// 1. Login as Smith
    /// 2. Click the "Logout" at the top right of the screen
    /// 3. Assert that the login screen is now shown
    /// </summary>
    [CodedUITest]
    public class CodedUITest2
    {
        public CodedUITest2()
        {
        }

        [TestMethod]
        public void CodedUITestMethod1()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
            string projectPath =
                Directory.GetParent(
                    Directory.GetParent(
                        Directory.GetParent(
                            Directory.GetParent(
                                Directory.GetParent(
                                    Directory.GetCurrentDirectory()
                                ).FullName
                            ).FullName
                        ).FullName
                     ).FullName
                ).FullName + @"\Client\bin\Debug\RentItClient.exe"
             ;

            System.Diagnostics.Process.Start(projectPath);

            this.UIMap.Test4LoginLogout();
            this.UIMap.Test4AssertLoginScreen();
            this.UIMap.Test4CloseWindow();
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

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

        private UIMap map;
    }
}
