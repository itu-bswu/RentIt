//-------------------------------------------------------------------------------------------------
// <copyright file="UnitTest2.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
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
        /// <summary>Tests the GetMovieInformation with valid inputs</summary>
        [TestMethod]
        public void GetMovieInformationTest()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();
                
                User testUser = db.Users.First(u => u.Username == "testUser");
                Movie testMovie = db.Movies.First(u => u.Title == "testMovie1");

                Movie foundMovie = service.GetMovieInformation(testUser.Token, testMovie.ID);

                Assert.AreEqual(testMovie.ID, foundMovie.ID);
                Assert.AreEqual(testMovie.Title, foundMovie.Title);
                Assert.AreEqual(testMovie.Description, foundMovie.Description);
                Assert.AreEqual(testMovie.Genre, foundMovie.Genre);
            }
        }

        /// <summary>
        /// Tests if an exception is thrown when an invalid token is given
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void InvalidTokenGetMovieInformationTest()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();

                Movie testMovie = db.Movies.First(u => u.Title == "testMovie1");

                Movie foundMovie = service.GetMovieInformation("Hello thar", testMovie.ID);
            }
        }

        /// <summary>
        /// Tests if null is returned with an invalid movieId or not
        /// </summary>
        [TestMethod]
        public void InvalidMovieIdGetMovieInformation()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();

                User testUser = db.Users.First(u => u.Username == "testUser");

                Movie foundMovie = service.GetMovieInformation(testUser.Token, 1337);

                Assert.IsNull(foundMovie);
            }
        }

        /// <summary>
        /// Tests the editing of the information of a movie
        /// Fails because of whitespace issues currently
        /// </summary>
        [TestMethod]
        public void EditMovieInformationTest()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();

                User testUser = db.Users.First(u => u.Username == "testAdmin");
                Movie testMovie = db.Movies.First(u => u.Title == "testMovie1");

               var newMovie = new Movie()
                   {
                       ID = testMovie.ID,
                       Description = "How to troll, for people new to the art",
                       FilePath = "You no take file location!",
                       Genre = "NoGenre",
                       ImagePath = "N/A",
                       Rentals = new Collection<Rental>(),
                       Title = "Trolling for beginners"
                   };

                service.EditMovieInformation(testUser.Token, newMovie);

                Movie foundMovie = db.Movies.First(u => u.Title == "Trolling for beginners");

                // This part can be removed when the whitespace issue has been fixed
                if (db.Movies.First(u => u.Title == "Trolling for beginners") != null)
                {
                    db.Movies.Remove(db.Movies.First(u => u.Title == "Trolling for beginners"));
                    db.SaveChanges();
                }

                Assert.AreEqual(foundMovie.Title, "Trolling for beginners");
                Assert.AreEqual(foundMovie.Description, "How to troll, for people new to the art");
                Assert.AreEqual(foundMovie.Genre, "NoGenre");
                Assert.AreEqual(foundMovie.FilePath, "You no take file location!");

                if (db.Movies.First(u => u.Title == "Trolling for beginners") != null)
                {
                    db.Movies.Remove(db.Movies.First(u => u.Title == "Trolling for beginners"));
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Tests if an exception is thrown when trying to edit movie
        /// information as a normal user
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientAccessLevelException))]
        public void InvalidUserTypeEditMovieInformationTest()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();

                User testUser = db.Users.First(u => u.Username == "testUser");
                Movie testMovie = db.Movies.First(u => u.Title == "testMovie1");

                var newMovie = new Movie()
                {
                    ID = testMovie.ID,
                    Description = "How to troll, for people new to the art",
                    FilePath = "You no take file location!",
                    Genre = "NoGenre",
                    ImagePath = "N/A",
                    Rentals = new Collection<Rental>(),
                    Title = "Trolling for beginners"
                };

                service.EditMovieInformation(testUser.Token, newMovie);

                if (db.Movies.First(u => u.Title == "Trolling for beginners") != null)
                {
                    db.Movies.Remove(db.Movies.First(u => u.Title == "Trolling for beginners"));
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Tests if the information is saved if an invalid movieId is given
        /// as a parameter. The information shouldn't be saved.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NoMovieFoundException))]
        public void InvalidMovieIdTypeEditMovieInformationTest()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();

                User testUser = db.Users.First(u => u.Username == "testAdmin");

                var newMovie = new Movie()
                {
                    ID = 1337,
                    Description = "How to troll, for people new to the art",
                    FilePath = "You no take file location!",
                    Genre = "NoGenre",
                    ImagePath = "N/A",
                    Rentals = new Collection<Rental>(),
                    Title = "Trolling for beginners"
                };

                service.EditMovieInformation(testUser.Token, newMovie);

                if (db.Movies.First(u => u.Title == "Trolling for beginners") != null)
                {
                    db.Movies.Remove(db.Movies.First(u => u.Title == "Trolling for beginners"));
                    db.SaveChanges();
                }
            }
        }

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