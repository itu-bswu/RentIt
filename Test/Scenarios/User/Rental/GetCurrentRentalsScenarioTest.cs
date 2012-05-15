// ------------------------------------------------------------------------------------------------
// <copyright file="GetCurrentRentalsScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Rental
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentIt.Tests.Utils;
    using RentItService;
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
        ///     1. Verify that the current rentals are current and belong to 'Smith'.
        ///     2. Check the number of rentals 'Smith' has.
        ///     3. Check the number of current rentals 'Smith' has.
        ///     4. Add a current rental and a non-current rental to 'Smith'.
        ///     5. Verify that that the number of rentals has gone up by two and the current rentals has only gone up by one.
        ///     6. Verify that the current rentals are all current.
        /// </para>
        /// </summary>
        [TestMethod] // TODO: Add test of limits
        public void GetCurrentRentalsTest()
        {
            var smith = User.Login(TestUser.User.Username, TestUser.User.Password);

            int daysToRent = 7;
            int rentalsCount = smith.Rentals.Count;
            int currentRentalsCount = smith.Rentals.Count(r => r.Time.AddDays(daysToRent) > DateTime.Now);

            Assert.IsTrue(smith.GetCurrentRentals().All(r => r.Time.AddDays(daysToRent) > DateTime.Now), "The 'current rentals' are not current.");
            Assert.IsTrue(smith.GetCurrentRentals().All(r => r.UserID == smith.ID), "One or more of the current rentals do not belong to the the user 'Smith'.");

            var rent1 = new Rental
                {
                    UserID = smith.ID,
                    EditionID = Movie.All.First(m => m.Title == "The Matrix").Editions.First().ID,
                    Time = DateTime.Now
                };

            var rent2 = new Rental
                {
                    UserID = smith.ID,
                    EditionID = Movie.All.First(m => m.Title == "Die Hard").Editions.First().ID,
                    Time = new DateTime(1753, 5, 15, 0, 0, 0)
                };

            RentItContext.Db.Rentals.Add(rent1);
            RentItContext.Db.Rentals.Add(rent2);
            RentItContext.Db.SaveChanges();
            RentItContext.ReloadDb();

            smith = User.GetByToken(smith.Token);
            var rentalsCount1 = smith.Rentals.Count;
            var currentRentalsCount1 = smith.Rentals.Count(r => r.Time.AddDays(daysToRent) > DateTime.Now);

            Assert.AreEqual(rentalsCount + 2, rentalsCount1, "The amount of rentals did not increase by 2.");
            Assert.AreEqual(currentRentalsCount + 1, currentRentalsCount1, "The current rentals did not increase by 1.");

            Assert.IsTrue(smith.GetCurrentRentals().All(r => r.Time.AddDays(daysToRent) > DateTime.Now), "The 'current rentals' are not current.");
        }
    }
}
