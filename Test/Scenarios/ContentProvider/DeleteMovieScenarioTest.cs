// -----------------------------------------------------------------------
// <copyright file="DeleteMovieScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.ContentProvider
{
    using System;
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
        /// 
        /// Pre-condtions:
        ///     1. A released movie exists in the database.
        /// 
        /// Steps:
        ///     1. Login as a content provider.
        ///     2. Attempt to delete the movie.
        ///     3. Verify that the movie no longer exists in the database.
        /// </summary>
        [TestMethod]
        public void DeleteMovieTest()
        {
            // Step 1
            var user = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            // Pre-condition 1
            var movie = Movie.All.First(m => m.Editions.Count > 0);

            // Step 2
            movie.Delete(user);

            // Step 3
            Assert.IsFalse(Movie.All.Any(m => m.ID == movie.ID), "Movie is still in the database.");
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
            var testMovie = Movie.All.First();

            // Step 1
            var user1 = User.Login(TestUser.User.Username, TestUser.User.Password);
                
            // Step 2
            testMovie.Delete(user1);
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
            const string Username = "SomeContentPublisher";
            const string Password = "12345";

            // Step 1
            var movie = Movie.All.First();

            // Step 2
            User.SignUp(new User
            {
                Username = Username,
                Password = Password,
                Email = "publisher@somecompany.org"
            });

            User.All.First(u => u.Username == Username).Type = UserType.ContentProvider;
            RentItContext.Db.SaveChanges();

            // Step 3
            var user = User.Login(Username, Password);

            // Step 4
            movie.Delete(user);
        }

        /// <summary>
        /// Purpose: Verify that it is possible to delete a movie edition.
        /// 
        /// Pre-conditions:
        ///     1. A content provider exists in the database.
        ///     2. One or more movies are uploaded by the content provider from step 1.
        ///     3. One or more editions exists for the movie in step 2.
        /// 
        /// Steps:
        ///     1. Login as the content provider from pre-condition 1.
        ///     2. Delete edition from pre-condition 3.
        /// </summary>
        [TestMethod]
        public void DeleteMovieEdition()
        {
            // Pre-condition 1 / Step 1
            var user = User.Login(TestUser.ContentProvider);

            // Pre-condition 2
            var movie = user.UploadedMovies.First();

            // Pre-cdontion 3
            var edition = movie.Editions.First();

            // Step 2
            edition.Delete(user);
        }

        /// <summary>
        /// Purpose: Verify that it is not possible to delete a movie edition, from 
        ///          from another content provider.
        /// 
        /// Pre-conditions:
        ///     1. A content provider exists in the database.
        ///     2. One or more movies are uploaded by the content provider from pre-condition 1.
        ///     3. One or more editions exists for the movie in pre-condition 2.
        /// 
        /// Steps:
        ///     1. Create a new content provider.
        ///     2. Login as the content provider from step 1.
        ///     3. Delete edition from pre-condition 3.
        ///     4. Verify it is not possible.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientRightsException))]
        public void DeleteMovieEditionFromOtherProvider()
        {
            string username = "NewProvider", password = "SomePasswordForNewProvider";

            // Pre-condition 2
            var movie = Movie.All.First();

            // Pre-cdontion 3
            var edition = movie.Editions.First();

            // Step 1
            User.SignUp(new User
            {
                Username = username,
                Password = password,
                Email = "careless@password.org"
            });

            // Step 2
            var user = User.Login(username, password);

            // Step 3
            edition.Delete(user);
        }
    }
}
