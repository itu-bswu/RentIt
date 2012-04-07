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

                Assert.AreEqual("Trolling for beginners", foundMovie.Title);
                Assert.AreEqual("How to troll, for people new to the art", foundMovie.Description);
                Assert.AreEqual("NoGenre", foundMovie.Genre);
                Assert.AreEqual("You no take file location!", foundMovie.FilePath);

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
    }
}