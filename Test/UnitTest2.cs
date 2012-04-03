//-------------------------------------------------------------------------------------------------
// <copyright file="UnitTest2.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;

    /// <summary>
    /// </summary>
    [TestClass]
    public class UnitTest2
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
                TestHelper.SetUpTestUsers();

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
        public void InsufficientExceptionEditProfileTest()
        {
            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestUsers();

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
        [ExpectedException(typeof(NullReferenceException))]
        public void InvalidInputEditProfileTest()
        {
            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestUsers();

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

        /// <summary>
        /// Tests a successful rent of a movie.
        /// </summary>
        [TestMethod]
        public void RentMovieTest()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            string testToken;
            int testID;

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUser");
                Movie movie = db.Movies.First(m => m.Description.Equals("testMovie1"));

                testToken = user.Token;
                testID = movie.ID;

                Assert.IsFalse(db.Rentals.Any(r => r.UserID == User.GetByToken(testToken).ID & r.MovieID == testID));

                User.RentMovie(testToken, testID);
            }

            using (var db = new RentItContext())
            {
                Assert.IsTrue(db.Rentals.Any(r => r.UserID == User.GetByToken(testToken).ID & r.MovieID == testID));

                db.Rentals.Remove(db.Rentals.First(r => r.UserID == User.GetByToken(testToken).ID & r.MovieID == testID));
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Tests for a rent where the user renting is not of type "User".
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotAUserException))]
        public void NotAUserRentMovieTest()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testContentProvider");
                Movie movie = db.Movies.First(m => m.Description.Equals("testMovie1"));

                string testToken = user.Token;
                int testID = movie.ID;

                User.RentMovie(testToken, testID);
            }
        }

        /// <summary>
        /// Tests for invalid input in a rent movie action.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void InvalidInputRentMovieTest()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            using (var db = new RentItContext())
            {
                Movie movie = db.Movies.First(m => m.Description.Equals("testMovie1"));

                int testID = movie.ID;

                User.RentMovie(null, testID);
            }
        }
    }
}