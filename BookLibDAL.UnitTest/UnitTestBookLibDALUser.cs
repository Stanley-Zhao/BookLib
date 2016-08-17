using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookLibDAL.UnitTest
{
    [TestClass]
    public class UnitTestBookLibDALUser
    {
        #region Members and Setup / Teardown
        private static TestContext tc = null;

        /// <summary>
        /// Nothing special in Initialization for now.
        /// </summary>
        /// <param name="pTestContext"></param>
        [ClassInitialize]
        public static void TestStartUp(TestContext pTestContext)
        {
            tc = pTestContext;
            Console.WriteLine("Start UnitTestBookLibUser");
        }

        /// <summary>
        /// Nothing special in Initialization for now.
        /// </summary>
        /// <param name="pTestContext"></param>
        [ClassCleanup]
        public static void TestTearDown()
        {
            // No code here
        }
        #endregion

        #region Test - User (Test: Create / Read / Update and Delete -> CRUD)
        /// <summary>
        /// Create a new user in system (user1)
        /// </summary>
        [TestMethod]
        public void TestCreateUser()
        {
            Console.WriteLine("Run TestCreateUser()");

            using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
            {
                try
                {
                    var usersList = from c in container.Users
                                    orderby c.Id
                                    select new { c.Id, c.Name, c.Email };

                    Assert.AreEqual(0, usersList.Count());

                    User newUser = new User() { Name = "user1", Email = "test@advent_test.com", Histories = null };

                    container.Users.Add(newUser);
                    int savedItemsCount = container.SaveChanges();

                    Assert.AreEqual(1, savedItemsCount);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.ToString());
                }
            }

            Console.WriteLine("Done of TestCreateUser()");
        }

        /// <summary>
        /// Read new user from system (user1)
        /// </summary>
        [TestMethod]
        public void TestReadUser()
        {
            Console.WriteLine("Run TestReadUser()");

            using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
            {

                var usersList = from c in container.Users
                                orderby c.Id
                                select new { c.Id, c.Name, c.Email };

                Assert.AreEqual(1, usersList.Count());

                var users = usersList.ToList();

                Assert.AreEqual("user1", users[0].Name);
                Assert.AreEqual("test@advent_test.com", users[0].Email);
            }

            Console.WriteLine("Run TestReadUser()");
        }

        /// <summary>
        /// Update new user from system (user1 -> user2)
        /// </summary>
        [TestMethod]
        public void TestUpdateUser()
        {
            Console.WriteLine("Run TestUpdateUser()");

            using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
            {
                User user = container.Users.Where(c => c.Name == "user1").FirstOrDefault() as User;

                Assert.IsNotNull(user);
                Assert.AreEqual("user1", user.Name);

                user.Name = "user2";

                int updatedItemsCount = container.SaveChanges();

                Assert.AreEqual(1, updatedItemsCount);
            }

            Console.WriteLine("Done of TestUpdateUser()");
        }

        /// <summary>
        /// Delete user from system (user2)
        /// </summary>
        [TestMethod]
        public void TestDeleteUser()
        {
            Console.WriteLine("Run TestDeleteUser()");

            using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
            {
                User user = container.Users.Where(c => c.Name == "user2").FirstOrDefault() as User;

                Assert.IsNotNull(user);
                Assert.AreEqual("user2", user.Name);

                container.Users.Remove(user);

                int deletedItemsCount = container.SaveChanges();

                Assert.AreEqual(1, deletedItemsCount);

                // check again, nothing in DB
                var userLists = (from c in container.Users
                                 orderby c.Id
                                 select new { c.Id, c.Name, c.Email });

                Assert.AreEqual(0, userLists.Count());
            }

            Console.WriteLine("Done of TestDeleteUser()");
        }
        #endregion
    }
}
