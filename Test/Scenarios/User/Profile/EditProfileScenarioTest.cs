// -----------------------------------------------------------------------
// <copyright file="EditProfileScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Profile
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;

    /// <summary>
    /// Scenario tests for the EditProfile functionality.
    /// </summary>
    [TestClass]
    public class EditProfileScenarioTest : DataTest
    {
        /// <summary>
        /// Test for successful edit of profile.
        /// </summary>
        [TestMethod]
        public void EditProfileTest()
        {
            string oldPassword;
            string oldName;
            string oldEmail;

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUser");

                Assert.AreEqual("test.dk", user.Password);
                Assert.AreEqual("Test User", user.FullName);
                Assert.AreEqual("testUser@testing.dk", user.Email);

                oldPassword = user.Password;
                oldName = user.FullName;
                oldEmail = user.Email;

                User user2 = new User
                {
                    Email = user.Email.ToUpper(),
                    FullName = user.FullName.ToUpper(),
                    ID = user.ID,
                    Password = user.Password.ToUpper(),
                    Rentals = user.Rentals,
                    Token = user.Token,
                    Type = user.Type,
                    TypeValue = user.TypeValue,
                    Username = user.Username
                };

                User.EditProfile(user2.Token, user2);
            }

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUser");

                Assert.AreEqual(oldPassword.ToUpper(), user.Password);
                Assert.AreEqual(oldName.ToUpper(), user.FullName);
                Assert.AreEqual(oldEmail.ToUpper(), user.Email);

                user.Password = "test.dk";
                user.FullName = "Test User";
                user.Email = "testUser@testing.dk";
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Test for someone editing a profile that is not his own.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientAccessLevelException))]
        public void InsufficientAccessEditProfileTest()
        {
            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUser");
                User user1 = db.Users.First(u => u.Username == "testContentProvider");

                User user2 = new User
                {
                    Email = user.Email.ToUpper(),
                    FullName = user.FullName.ToUpper(),
                    ID = user.ID,
                    Password = user.Password.ToUpper(),
                    Rentals = user.Rentals,
                    Token = user.Token,
                    Type = user.Type,
                    TypeValue = user.TypeValue,
                    Username = user.Username
                };

                User.EditProfile(user1.Token, user2);
            }
        }

        /// <summary>
        /// Tests for invald input.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidInputEditProfileTest()
        {
            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUser");

                User user2 = new User
                {
                    Email = user.Email.ToUpper(),
                    FullName = null,
                    ID = user.ID,
                    Password = null,
                    Rentals = user.Rentals,
                    Token = user.Token,
                    Type = user.Type,
                    TypeValue = user.TypeValue,
                    Username = user.Username
                };

                User.EditProfile(user.Token, user2);
            }
        }
    }
}
