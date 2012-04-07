// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetContentProvidersTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the GetContentProvidersTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.UserInformationService
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentItService;
    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Exceptions;
    using RentItService.Services;

    /// <summary>
    /// Class for testing the GetContentProviders method
    /// </summary>
    [TestClass]
    public class GetContentPublishersTest : DataTest
    {
        /// <summary>
        /// Tests the GetContentPublishers method
        /// </summary>
        [TestMethod]
        public void GetContentProvidersTest1()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestUsers();

                User testAdmin = db.Users.First(u => u.Username == "testAdmin");
                User testProvider = db.Users.First(u => u.Username == "testContentProvider");

                Assert.IsNotNull(testProvider);

                var amountOfPublishers = Enumerable.Count(db.Users, user => user.Type == UserType.ContentProvider);
                var publisherList = service.GetContentPublishers(testAdmin.Token);

                Assert.IsNotNull(publisherList);
                Assert.AreEqual(amountOfPublishers, publisherList.Count());

                publisherList = service.GetContentPublishers(testAdmin.Token);

                Assert.IsNotNull(publisherList);
                Assert.AreEqual(testProvider.ID, publisherList.First(u => u.Username == "testContentProvider").ID);
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

                var userList = service.GetContentPublishers(testUser.Token);
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

                var userList = service.GetContentPublishers("bneiuwnvu9p28h3ny84o28uyh43892");
            }
        }
    }
}
