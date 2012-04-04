//-------------------------------------------------------------------------------------------------
// <copyright file="DataTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utils;

    /// <summary>
    /// Base test class with loaded test data. 
    /// Connects to the database specified in Web.config, 
    /// empties it and load the data-set.
    /// </summary>
    [TestClass]
    public abstract class DataTest : TestBase
    {
        /// <summary>
        /// Loads or reloads the data before each test.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            DataUtil.Load(DataSet.TestData);
        }
    }
}