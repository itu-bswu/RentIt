// -----------------------------------------------------------------------
// <copyright file="DeleteMovieScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.ContentProvider
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentIt.Tests.Utils;
    using RentItService;
    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Exceptions;

    /// <summary>
    /// Scenario tests for the delete movie functionality.
    /// </summary>
    [TestClass]
    public class DeleteMovieScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that deletion of a movie is possible.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A movie with the description "The Matrix" must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Make sure pre-conditions hold.
        ///     2. Get a content provider user.
        ///     3. Attempt to delete the movie.
        ///     4. Verify that the movie no longer exists in the database.
        ///     5. Add the movie back into the database.
        /// </summary>
        [TestMethod]
        public void DeleteMovieTest()
        {
            FileInfo fi;

            using (var db = new RentItContext())
            {
                Assert.IsTrue(db.Movies.Any(m => m.Title.Equals("The Matrix")), "Pre-condition 1 does not hold.");

                var testMovie = db.Movies.First(m => m.Title.Equals("The Matrix"));

                var user1 = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

                fi = new FileInfo(testMovie.FilePath);

                Movie.DeleteMovie(user1.Token, testMovie);
            }

            using (var db = new RentItContext())
            {
                Assert.IsFalse(db.Movies.Any(m => m.Title.Equals("The Matrix")), "Movie is still in the database.");
            }

            Assert.IsFalse(fi.Exists, "The file still exists in the file system.");
        }

        /// <summary>
        /// Purpose: Verify that only Content Providers can delete movies.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A movie with the title "The Matrix" must exist in the database.
        ///     2. A user with the user name "Smith" must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Make sure pre-conditions hold.
        ///     2. Try to delete the movie as the user.
        ///     3. Verify that InsufficientRightsException is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientRightsException))]
        public void InsufficientAccessDeleteMovieTest()
        {
            using (var db = new RentItContext())
            {
                Assert.IsTrue(db.Movies.Any(m => m.Title.Equals("The Matrix")), "Pre-condition 1 does not hold.");

                var testMovie = db.Movies.First(m => m.Title.Equals("The Matrix"));

                var user1 = User.Login(TestUser.User.Username, TestUser.User.Password);

                Movie.DeleteMovie(user1.Token, testMovie);
            }
        }

        /// <summary>
        /// Purpose: Verify that it is not possible to delete a null movie.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A user with the user name "Universal" must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Make sure pre-conditions hold.
        ///     2. Try to delete a null movie.
        ///     3. Verify ArgumentNullException is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidInputDeleteMovieTest()
        {
            TestHelper.SetUpTestUsers();

            var user1 = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            Movie.DeleteMovie(user1.Token, null);
        }

        /// <summary>
        /// Purpose: Verify that it is not possible to delete a movie 
        /// that belongs to another content publisher
        /// 
        /// Pre-condition:
        ///     1. A movie uploaded by some publisher exists in the database.
        /// 
        /// Steps:
        ///     1. Get a movie created by some user in the database.
        ///     2. Create a new content publisher.
        ///     3. Login as the new user.
        ///     4. Delete movie from step 1 with publisher from step 2.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientRightsException))]
        public void DeleteMovieFromOtherProvider()
        {
            using (var db = new RentItContext())
            {
                const string Username = "SomeContentPublisher";
                const string Password = "12345";

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
                Movie.DeleteMovie(user.Token, movie);
            }
        }
    }
}
