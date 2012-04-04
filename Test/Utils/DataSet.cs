//-------------------------------------------------------------------------------------------------
// <copyright file="DataSet.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Utils
{
    /// <summary>
    /// Collection of data-sets.
    /// TODO: Use config files?
    /// </summary>
    public static class DataSet
    {
        /// <summary>
        /// Gets filename of empty data-set. 
        /// Re-creates all tables from scratch, with no data.
        /// </summary>
        public static string Empty
        {
            get
            {
                return @"Dataset\Create.sql";
            }
        }

        /// <summary>
        /// Gets filename of test data-set. 
        /// Re-creates all tables from scratch, with no data.
        /// </summary>
        public static string TestData
        {
            get
            {
                return @"Dataset\Data.sql";
            }
        }
    }
}
