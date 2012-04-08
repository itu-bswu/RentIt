using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RentIt.Tests.Scenarios.UserInformationService
{
    using RentItService;
    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Exceptions;
    using RentItService.Services;

    /// <summary>
    /// Class for testing the GetUsers method
    /// </summary>
    [TestClass]
    public class GetUsersTest : DataTest
    {
        /// <summary>
        /// Tests the GetUser method
        /// </summary>
        [TestMethod]
        public void GetUsersTest1()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestUsers();

                User testAdmin = db.Users.First(u => u.Username == "testAdmin");
                User testUser = db.Users.First(u => u.Username == "testUser");

                var amountOfUsers = Enumerable.Count(db.Users, user => user.Type == UserType.User);
                var userList = service.GetUsers(testAdmin.Token);

                Assert.IsNotNull(userList);
                Assert.AreEqual(amountOfUsers, userList.Count());

                userList = service.GetUsers(testAdmin.Token);

                Assert.IsNotNull(userList);
                Assert.AreEqual(testUser.ID, userList.First(u => u.Username == "testUser").ID);
            }
        }

        /// <summary>
        /// Tests if the method throws the right exception when
        /// calling it with a wrong user type
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientAccessLevelException))]
        public void InvalidUserTypeTest()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestUsers();

                User testUser = db.Users.First(u => u.Username == "testUser");

                var userList = service.GetUsers(testUser.Token);
            }
        }

        /// <summary>
        /// Tests if the method throws the right exception when
        /// calling it with an invalid token
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void InvalidTokenTest()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestUsers();

                var userList = service.GetUsers("bneiuwnvu9p28h3ny84o28uyh43892");
            }
        }
    }
}
