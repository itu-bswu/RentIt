// -----------------------------------------------------------------------
// <copyright file="GetCurrentRentalsScenarioTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Rental
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService.Entities;

    /// <summary>
    /// Scenario tests for the Get Current Rentals 
    /// </summary>
    [TestClass]
    public class GetCurrentRentalsScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that the user only gets current rentals and that they all belong to him.
        /// <para>
        /// Pre-condtions:
        ///     1. A user with the username 'Smith' must exist in the database.
        /// </para>
        /// <para>
        /// Steps:
        ///     1. Check the number of rentals 'Smith' has.
        ///     2. Check the number of current rentals 'Smith' has.
        ///     3. Verify that the current rentals are all current.
        ///     4. Add a non-current rental and a current rental to 'Smith'.
        ///     5. Verify that that the number of rentals has gone up by two and the current rentals has only gone up by one.
        ///     6. Verify that the current rentals are all current.
        /// </para>
        /// </summary>
        [TestMethod]
        public void GetCurrentRentalsTest()
        {

        }

        /// <summary>
        /// Purpose: Verify that a user only gets his own rentals.
        /// <para>
        /// Pre-condtions:
        ///     1. 
        ///     2. 
        /// </para>
        /// <para>
        /// Steps:
        ///     1. 
        ///     2. 
        ///     3. 
        ///     4. 
        ///     5. 
        /// </para>
        /// </summary>
        [TestMethod]
        public void DifferentUserGetCurrentRentalsTest()
        {

        }

        /*
        /// <summary>
        /// Purpose: 
        /// <para>
        /// Pre-condtions:
        ///     1. 
        ///     2. 
        /// </para>
        /// <para>
        /// Steps:
        ///     1. 
        ///     2. 
        ///     3. 
        ///     4. 
        ///     5. 
        /// </para>
        /// </summary>
        [TestMethod]
        public void TODO: Test here!
        */
    }
}
