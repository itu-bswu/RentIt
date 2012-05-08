// -----------------------------------------------------------------------
// <copyright file="DeleteMovieScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.ContentProvider
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentIt.Tests.Utils;
    using RentItService;
    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Exceptions;
    using Tools;

    /// <summary>
    /// Scenario tests for the delete movie functionality.
    /// </summary>
    [TestClass]
    public class DeleteMovieScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that deletion of a movie is possible.
        /// 
        /// Pre-condtions:
        ///     1. A released movie exists in the database.
        /// 
        /// Steps:
        ///     1. Login as a content provider.
        ///     2. Attempt to delete the movie.
        ///     3. Verify that the movie no longer exists in the database.
        ///     4. Verify that the files has been deleted.
        /// </summary>
        [TestMethod]
        public void DeleteMovieTest()
        {
            // Step 1
            var user = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            // Pre-condition 1
            var movie = Movie.GetAllMovies(user.Token).First(m => m.Editions.Count > 0);
            var files = movie.Editions.Select(edition => edition.FilePath).ToList();

            // Step 2
            Movie.DeleteMovie(user.Token, movie);

            using (var db = new RentItContext())
            {
                // Step 3
                Assert.IsFalse(db.Movies.Any(m => m.ID == movie.ID), "Movie is still in the database.");
            }

            // Step 4
            foreach (var filePath in files.Select(file => Constants.UploadDownloadFileFolder + file))
            {
                Assert.IsFalse(File.Exists(filePath), "File has not been deleted.");
            }
        }

        /// <summary>
        /// Purpose: Verify that only Content Providers can delete movies.
        /// 
        /// Steps:
        ///     1. Login as a normal user.
        ///     1. Try to delete the movie as the normal user.
        ///     2. Verify that InsufficientRightsException is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientRightsException))]
        public void InsufficientAccessDeleteMovieTest()
        {
            using (var db = new RentItContext())
            {
                var testMovie = db.Movies.First();

                // Step 1
                var user1 = User.Login(TestUser.User.Username, TestUser.User.Password);
                
                // Step 2
                Movie.DeleteMovie(user1.Token, testMovie);
            }
        }

        /// <summary>
        /// Purpose: Verify that it is not possible to delete a null movie.
        /// 
        /// Steps:
        ///     1. Login as a content provider.
        ///     2. Try to delete a null movie.
        ///     3. Verify ArgumentNullException is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidInputDeleteMovieTest()
        {
            // Step 1
            var user1 = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            // Step 2
            Movie.DeleteMovie(user1.Token, null);
        }

        /// <summary>
        /// Purpose: Verify that it is not possible to delete a movie 
        /// that belongs to another content provider.
        /// 
        /// Steps:
        ///     1. Get a movie created by some content provider in the database.
        ///     2. Create a new content provider.
        ///     3. Login as the new content provider.
        ///     4. Delete movie from step 1 with provider from step 2.
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
