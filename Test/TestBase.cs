//-------------------------------------------------------------------------------------------------
// <copyright file="TestBase.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utils;

    /// <summary>
    /// Base test class. 
    /// Connects to the database specified in Web.config, 
    /// and empties it. Will not load any data-set.
    /// </summary>
    [TestClass]
    public abstract class TestBase
    {
        /// <summary>
        /// Empty the database before each test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            DataUtil.Empty();
        }
    }
}