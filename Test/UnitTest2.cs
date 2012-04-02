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
    using RentItService.Services;

    /// <summary>
    /// </summary>
    [TestClass]
    public class UnitTest2
    {
        /// <summary>
        /// </summary>
        [TestMethod]
        public void EditProfileTest()
        {
            Service service = new Service();
            string oldPassword;
            string oldName;
            string oldEmail;

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestUsers();

                User user = db.Users.First(u => u.Username == "testUser");

                Assert.AreEqual("test.dk", user.Password.TrimEnd(Convert.ToChar(" ")));
                Assert.AreEqual("Test User", user.FullName.TrimEnd(Convert.ToChar(" ")));
                Assert.AreEqual("testUser@testing.dk", user.Email.TrimEnd(Convert.ToChar(" ")));

                oldPassword = user.Password;
                oldName = user.FullName;
                oldEmail = user.Email;

                User user2 = new User()
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
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientAccessLevelException))]
        public void InsufficientExceptionEditProfileTest()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestUsers();

                User user = db.Users.First(u => u.Username == "testUser");
                User user1 = db.Users.First(u => u.Username == "testContentProvider");

                User user2 = new User()
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
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void InvalidInputEditProfileTest()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestUsers();

                User user = db.Users.First(u => u.Username == "testUser");

                User user2 = new User()
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

        [TestMethod]
        public void RentMovieTest()
        {
            Service service = new Service();
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

        [TestMethod]
        [ExpectedException(typeof(NotAUserException))]
        public void NotAUserRentMediaTest()
        {
            Service service = new Service();
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            string testToken;
            int testID;

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testContentProvider");
                Movie movie = db.Movies.First(m => m.Description.Equals("testMovie1"));

                testToken = user.Token;
                testID = movie.ID;

                User.RentMovie(testToken, testID);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void InvalidInputRentMediaTest()
        {
            Service service = new Service();
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            string testToken;
            int testID;

            using (var db = new RentItContext())
            {
                Movie movie = db.Movies.First(m => m.Description.Equals("testMovie1"));

                testID = movie.ID;

                User.RentMovie(null, testID);
            }
        }
    }
}