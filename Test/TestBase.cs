﻿//-------------------------------------------------------------------------------------------------
// <copyright file="TestBase.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests
{
    using System.Transactions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentIt.Tests.Utils;

    /// <summary>
    /// Base test class. 
    /// Connects to the database specified in Web.config, 
    /// and empties it. Will not load any data-set.
    /// </summary>
    [TestClass]
    public class TestBase
    {
        /// <summary>
        /// Transaction scope.
        /// </summary>
        //private TransactionScope scope;
        // TODO: Look into transactions to speed up test running.

        /// <summary>
        /// Initializes a new instance of the <see cref="TestBase"/> class. 
        /// Empties the database.
        /// </summary>
        /*public TestBase()
        {
            using (var datautil = new DataUtil())
            {
                datautil.Empty();
            }
        }*/

        /// <summary>
        /// Initializes a new transaction for each test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            //this.scope = new TransactionScope();

            // TODO: Temporary fix
            using (var datautil = new DataUtil())
            {
                datautil.Empty();
            }
        }

        /// <summary>
        /// Dispose the transaction after a test is run.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            RentItService.RentItContext.ReloadDb();
            //this.scope.Dispose();
        }
    }
}