using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity.Infrastructure;

namespace BookLibDAL.UnitTest
{
    [TestClass]
    public class UnitTestBookLibDALUser : UnitTestBase
    {
        #region Test - User (Test: Create / Read / Update and Delete -> CRUD)
        /// <summary>
        /// Create a new user in system (user1, test@advent_test.com)
        /// </summary>
        [TestMethod]
        public void TestCreateUser()
        {
            StartTest(nameof(TestCreateUser).ToString());

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

            EndTest(nameof(TestCreateUser).ToString());
        }

        /// <summary>
        /// Create a new user in system (user2, test@advent_test.com), this time, there will be an error from DB say user email is not allowed to be duplicated.
        /// </summary>
        [TestMethod]
        public void TestCreateDuplicateUser()
        {
            StartTest(nameof(TestCreateDuplicateUser).ToString());

            using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
            {
                try
                {
                    var usersList = from c in container.Users
                                    orderby c.Id
                                    select new { c.Id, c.Name, c.Email };

                    Assert.AreEqual(0, usersList.Count());

                    User newUser = new User() { Name = "user2", Email = "test@advent_test.com", Histories = null };

                    container.Users.Add(newUser);
                    int savedItemsCount = container.SaveChanges();

                    if (savedItemsCount == 1)
                    {
                        Assert.Fail("Supposed to get exception from Database but not");
                    }
                }
                catch (Exception ex)
                {
                    if(!(ex is DbUpdateException))
                    {
                        Assert.Fail("Supposed to get DbUpdateException from Database but not");
                    }
                }
            }

            EndTest(nameof(TestCreateDuplicateUser).ToString());
        }

        /// <summary>
        /// Read new user from system (user1)
        /// </summary>
        [TestMethod]
        public void TestReadUser()
        {
            StartTest(nameof(TestReadUser).ToString());

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

            EndTest(nameof(TestReadUser).ToString());
        }

        /// <summary>
        /// Update new user from system (user1 -> user2)
        /// </summary>
        [TestMethod]
        public void TestUpdateUser()
        {
            StartTest(nameof(TestUpdateUser).ToString());

            using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
            {
                User user = container.Users.Where(c => c.Name == "user1").FirstOrDefault() as User;

                Assert.IsNotNull(user);
                Assert.AreEqual("user1", user.Name);

                user.Name = "user2";

                int updatedItemsCount = container.SaveChanges();

                Assert.AreEqual(1, updatedItemsCount);
            }

            EndTest(nameof(TestUpdateUser).ToString());
        }

        /// <summary>
        /// Delete user from system (user2)
        /// </summary>
        [TestMethod]
        public void TestDeleteUser()
        {
            StartTest(nameof(TestDeleteUser).ToString());

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

            EndTest(nameof(TestDeleteUser).ToString());
        }
        #endregion
    }
}
