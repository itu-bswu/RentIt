//-------------------------------------------------------------------------------------------------
// <copyright file="SignUpScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Profile
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Exceptions;

    /// <summary>
    /// Scenario tests of the SignUp feature.
    /// </summary>
    [TestClass]
    public class SignUpScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that it is possible to sign up.
        /// 
        /// Steps:
        ///     1. Create an instance of the User class.
        ///     2. Fill with valid information.
        ///     3. Sign up the user.
        ///     4. Verify that the user is given an ID, and is of type User.
        ///     5. Verify that no rentals are registered for the new user.
        ///     6. Verify that all information are transfered properly.
        /// </summary>
        [TestMethod]
        public void SignUpWithValidInfo()
        {
            // Arrange
            var user = new User
            {
                Username = "UniqueUsername",
                FullName = "Unique Username",
                Email = "unique@username.com",
                Password = "Test1234"
            };

            // Act
            var result = User.SignUp(user);

            // Assert
            Assert.IsTrue(result.ID > 0, "Wrong or no ID given to new user upon signup!");
            Assert.AreEqual(result.Type, UserType.User, "Wrong user-type given to user upon signup!");
            Assert.AreEqual(result.Rentals.Count, 0, "Incorrect amount of rentals for new user!");

            Assert.AreEqual(result.Username, user.Username, "Wrong username!");
            Assert.AreEqual(result.FullName, user.FullName, "Wrong name!");
            Assert.AreEqual(result.Email, user.Email, "Wrong email!");
            Assert.AreEqual(result.Password, user.Password, "Wrong password!");
        }

        /// <summary>
        /// Purpose: Verify that type is automatically set to user, 
        /// token is reset and ID is auto-generated when trying to 
        /// set those settings to invalid values.
        /// 
        /// Steps:
        ///     1. Create an instance of the User class.
        ///     2. Fill with invalid Type, ID and Token.
        ///     3. Sign up the user.
        ///     4. Verify user type is now user.
        ///     5. Verify ID has been generated.
        ///     6. Verify token has been reset.
        /// </summary>
        [TestMethod]
        public void SignUpWithInvalidInfo()
        {
            const int InitialID = 13371337;

            // Arrange
            var user = new User
            {
                Username = "UniqueUsername",
                FullName = "Unique Username",
                Email = "unique@username.com",
                Password = "Test1234",
                Type = UserType.SystemAdmin,
                ID = InitialID,
                Token = "a1b2c3d4"
            };

            // Act
            var result = User.SignUp(user);

            // Assert
            Assert.AreEqual(result.Type, UserType.User, "User type has not changed to user!");
            Assert.AreNotEqual(result.ID, InitialID, "User ID har not changed!");
            Assert.AreEqual(result.Token, string.Empty, "Token has not been reset!");
        }

        /// <summary>
        /// Purpose: Verify that username has to be set.
        /// 
        /// Steps:
        ///     1. Create an instance of the User class.
        ///     2. Fill with valid info, but empty username.
        ///     3. Signup the user.
        ///     4. Verify exception is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SignUpWithEmptyUsername()
        {
            // Arrange
            var user = new User
            {
                FullName = "Unique Username",
                Email = "unique@username.com",
                Password = "Test1234"
            };

            // Act
            User.SignUp(user);
        }

        /// <summary>
        /// Purpose: Verify that email has to be set.
        /// 
        /// Steps:
        ///     1. Create an instance of the User class.
        ///     2. Fill with valid info, but empty email.
        ///     3. Signup the user.
        ///     4. Verify exception is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SignUpWithEmptyEmail()
        {
            // Arrange
            var user = new User
            {
                Username = "UniqueUsername",
                FullName = "Unique Username",
                Password = "Test1234"
            };

            // Act
            User.SignUp(user);
        }

        /// <summary>
        /// Purpose: Verify that password has to be set.
        /// 
        /// Steps:
        ///     1. Create an instance of the User class.
        ///     2. Fill with valid info, but empty password.
        ///     3. Signup the user.
        ///     4. Verify exception is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SignUpWithEmptyPassword()
        {
            // Arrange
            var user = new User
            {
                Username = "UniqueUsername",
                FullName = "Unique Username",
                Email = "unique@username.com",
            };

            // Act
            User.SignUp(user);
        }

        /// <summary>
        /// Purpose: Verify that it is not possible to signup 
        /// with a username that is already in use.
        /// 
        /// Pre-conditions:
        ///     1. A user with some username exists in the database.
        /// 
        /// Steps:
        ///     1. Create an instance of the User class.
        ///     2. Fill it with valid information, but with the same username as pre-condition 1.
        ///     3. Validate sign up is not possible.
        /// </summary>
        /*[TestMethod]
        [ExpectedException(typeof(UsernameInUseException))]
        public void SignUpWithExistingUsername()
        {
            //TODO: setup test rentals

            // Arrange
            /*var existingUser = TestHelper.SetUpTestUsers().First();

            var user = new User
            {
                Username = existingUser.Username,
                FullName = "Unique Username",
                Email = "unique@username.com",
                Password = "Test1234"
            };

            // Act
            User.SignUp(user);*/
        //}
    }
}
