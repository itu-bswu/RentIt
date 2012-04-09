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

    using Tools;

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
        ///     1. Verify that the current rentals are current.
        ///     2. Check the number of rentals 'Smith' has.
        ///     3. Check the number of current rentals 'Smith' has.
        ///     4. Add a current rental and a non-current rental to 'Smith'.
        ///     5. Verify that that the number of rentals has gone up by two and the current rentals has only gone up by one.
        ///     6. Verify that the current rentals are all current.
        /// </para>
        /// </summary>
        [TestMethod]
        public void GetCurrentRentalsTest()
        {
            var smith = User.Login(TestUser.User.Username, TestUser.User.Password);

            using (var db = new RentItContext())
            {
                Assert.IsTrue(User.GetCurrentRentals(smith.Token).All(r => r.Time.AddDays(Constants.DaysToRent) > DateTime.Now), "The 'current rentals' are not current.");

                var rentalsCount = db.Users.First(u => u.ID == smith.ID).Rentals.Count;
                var currentRentalsCount =
                    db.Users.First(u => u.ID == smith.ID).Rentals.Count(r => r.Time.AddDays(Constants.DaysToRent) > DateTime.Now);

                var rent1 = new Rental
                    {
                        UserID = smith.ID,
                        MovieID = db.Movies.First(m => m.Title == "The Matrix").ID,
                        Time = DateTime.Now
                    };

                var rent2 = new Rental
                    {
                        UserID = smith.ID,
                        MovieID = db.Movies.First(m => m.Title == "Die Hard").ID,
                        Time = new DateTime(666, 5, 15, 0, 0, 0)
                    };

                db.Rentals.Add(rent1);
                db.Rentals.Add(rent2);
                db.SaveChanges();

                var rentalsCount1 = db.Users.First(u => u.ID == smith.ID).Rentals.Count;
                var currentRentalsCount1 =
                    db.Users.First(u => u.ID == smith.ID).Rentals.Count(r => r.Time.AddDays(Constants.DaysToRent) > DateTime.Now);

                Assert.AreEqual(rentalsCount + 2, rentalsCount1, "The amount of rentals did not increase by 2.");
                Assert.AreEqual(currentRentalsCount + 1, currentRentalsCount1, "The current rentals did not increase by 1.");

                Assert.IsTrue(User.GetCurrentRentals(smith.Token).All(r => r.Time.AddDays(Constants.DaysToRent) > DateTime.Now), "The 'current rentals' are not current.");
            }
        }

        /// <summary>
        /// Purpose: 
        /// <para>
        /// Pre-condtions:
        ///     1. 
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
