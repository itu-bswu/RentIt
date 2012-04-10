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
    public class DataTest : TestBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTest"/> class. 
        /// Loads or reloads the data.
        /// </summary>
        /*public DataTest()
        {
            using (var datautil = new DataUtil())
            {
                datautil.Load(DataSet.TestData);
            }
        }*/

        /// <summary>
        /// Loads data before each test.
        /// TODO: Temporary fix
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            // TODO: Temporary fix
            using (var datautil = new DataUtil())
            {
                datautil.Load(DataSet.TestData);
            }
        }
    }
}