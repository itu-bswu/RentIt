// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditMovieInformationTest.cs" company="RentIt">
//   Copyright (c) RentIt. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.ContentProvider
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService;
    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Exceptions;
    using RentItService.Services;
    using Utils;

    /// <summary>
    /// Class for testing the EditMovieInformation method
    /// </summary>
    [TestClass]
    public class EditMovieInformationTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that the method changes the values of the movie
        /// <para></para>
        /// Pre-condtions:
        ///     1. A movie must exist in the database.
        ///     2. An admin must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Find a movie in the databse.
        ///     3. Login as the admin.
        ///     4. Create a new movie with new values, but with 
        ///        the ID from the found movie.
        ///     5. Call EditMovieInformation with the with the 
        ///        token from the admin and the new movie.
        ///     6. Assert that a movie with the name "Trolling 
        ///        for beginners" exists in the database.
        /// </summary>
        [TestMethod]
        public void EditMovieInformationValidTest()
        {
            var service = new Service();

            using (var db = new RentItContext())
            {
                var testUser = TestUser.SystemAdmin;
                var testMovie = db.Movies.First();

                var loggedinUser = User.Login(testUser.Username, testUser.Password);

                var newMovie = new Movie
                    {
                        ID = testMovie.ID,
                        Description = "How to troll, for people new to the art",
                        FilePath = "You no take file location!",
                        Genre = "NoGenre",
                        ImagePath = "N/A",
                        Rentals = new Collection<Rental>(),
                        Title = "Trolling for beginners"
                    };

                service.EditMovieInformation(loggedinUser.Token, newMovie);

                Movie foundMovie = db.Movies.First(u => u.Title == "Trolling for beginners");

                Assert.AreEqual("Trolling for beginners", foundMovie.Title, "The titles doesn't match");
                Assert.AreEqual("How to troll, for people new to the art", foundMovie.Description, "The descriptions doesn't match");
                Assert.AreEqual("NoGenre", foundMovie.Genre, "The genre doesn't match");
            }
        }

        /// <summary>
        /// Purpose: Verify that the method throws the correct exception
        /// when called by an account with an insufficient user type
        /// <para></para>
        /// Pre-condtions:
        ///     1. A movie must exist in teh database.
        ///     2. A user must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Find a movie in the databse.
        ///     3. Login as the user.
        ///     4. Create a new movie with new values, but with 
        ///        the ID from the found movie.
        ///     5. Call EditMovieInformation with the with the 
        ///        token from the user and the new movie.
        ///     6. Assert that an InsufficientAccessLevelException
        ///        is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientRightsException))]
        public void EditMovieInformationInvalidUserTypeTest()
        {
            var service = new Service();

            using (var db = new RentItContext())
            {
                var testUser = TestUser.User;
                var testMovie = db.Movies.First();

                var loggedinUser = User.Login(testUser.Username, testUser.Password);

                var newMovie = new Movie
                    {
                        ID = testMovie.ID,
                        Description = "How to troll, for people new to the art",
                        FilePath = "You no take file location!",
                        Genre = "NoGenre",
                        ImagePath = "N/A",
                        Rentals = new Collection<Rental>(),
                        Title = "Trolling for beginners"
                    };

                service.EditMovieInformation(loggedinUser.Token, newMovie);
            }
        }

        /// <summary>
        /// Purpose: Verify that the method throws the correct exception
        /// when called by an account with an insufficient user type
        /// <para></para>
        /// Pre-condtions:
        ///     1. An admin must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Login as the admin.
        ///     2. Create a new movie with new values, and an
        ///        ID that doesn't corrospond to a movie in the 
        ///        databse.
        ///     3. Call EditMovieInformation with the with the 
        ///        token from the admin and the new movie.
        ///     4. Assert that a NoMovieFoundException
        ///        is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NoMovieFoundException))]
        public void EditMovieInformationInvalidMovieIdTypeTest()
        {
            var service = new Service();
            var testUser = TestUser.ContentProvider;

            var loggedinUser = User.Login(testUser.Username, testUser.Password);

            var newMovie = new Movie
                {
                    ID = 89485618,
                    Description = "How to troll, for people new to the art",
                    FilePath = "You no take file location!",
                    Genre = "NoGenre",
                    ImagePath = "N/A",
                    Rentals = new Collection<Rental>(),
                    Title = "Trolling for beginners"
                };

            service.EditMovieInformation(loggedinUser.Token, newMovie);
        }

        /// <summary>
        /// Purpose: Verify that it is not possible to edit a movie 
        /// uploaded by another content publisher.
        /// 
        /// Pre-condition:
        ///     1. A movie uploaded by some publisher exists in the database.
        /// 
        /// Steps:
        ///     1. Get a movie created by some user in the database.
        ///     2. Create a new content publisher.
        ///     3. Login as the new user.
        ///     4. Edit movie from step 1 with publisher from step 2.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientRightsException))]
        public void EditMovieFromOtherProvider()
        {
            using (var db = new RentItContext())
            {
                const string Username = "SomeContentPublisher";
                const string Password = "12345";

                var service = new Service();

                // Step 1
                var movie = db.Movies.First();

                // Step 2
                User.SignUp(new User
                {
                    Username = Username,
                    Password = Password,
                    Email = "publisher@somecompany.org"
                });

                db.Users.First(u => u.Username == Username).Type = UserType.ContentProvider;
                db.SaveChanges();

                // Step 3
                var user = User.Login(Username, Password);

                // Step 4
                service.EditMovieInformation(user.Token, movie);
            }
        }
    }
}