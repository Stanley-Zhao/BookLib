using BookLib.DataAccessLayer;
using BookLib.DataModel;
using BookLib.Delegates;
using Business.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace BookLibDAL.UnitTest
{
    [TestClass]
    public class UnitTestBookLibDALUser : UnitTestBase
    {
        #region CleanUp for testing method
        /// <summary>
        /// Clean up testing data in db.
        /// </summary>
        [TestInitialize]
        [TestCleanup]
        public override void CleanUp()
        {
            DoCleanUp(new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
                        {
                            container.Histories.RemoveRange(container.Histories);
                            container.Users.RemoveRange(container.Users.Where(x => x.Id > 1).ToList()); // Id=1, the root admin user
                            container.SaveChanges();
                        }
                    }
                ));
        }
        #endregion

        #region Test - User (Test: Create / Delete / Read and Update -> CRUD)
        /// <summary>
        /// Create a duplicate user in system (user2, test@advent_test.com), this time, there will be an error from DB say user email is not allowed to be duplicated.
        /// </summary>
        [TestMethod]
        public void TestCreateDuplicateUser()
        {
            DoTest(nameof(TestCreateDuplicateUser), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
                        {
                            try
                            {
                                // prepare testing data - create user1
                                CreateUser(container);
                                var usersList = from c in container.Users
                                                orderby c.Id
                                                select new { c.Id, c.Name, c.Email };
                                Assert.AreEqual(2, usersList.Count());
                                User newUser = new User() { Name = "user2", Email = "test@advent_test.com" };
                                container.Users.Add(newUser);
                                int savedItemsCount = container.SaveChanges();
                                if (savedItemsCount == 1)
                                {
                                    Assert.Fail("Supposed to get exception from Database but not");
                                }
                            }
                            catch (Exception ex)
                            {
                                if (!(ex is DbUpdateException))
                                {
                                    Assert.Fail("Supposed to get DbUpdateException from Database but not");
                                }
                                else
                                {
                                    Info("Get DbUpdateException, which is expected result");
                                }
                            }
                        }
                    }
                ));
        }

        /// <summary>
        /// Create a new user in system (user1, test@advent_test.com)
        /// </summary>
        [TestMethod]
        public void TestCreateUser()
        {
            DoTest(nameof(TestCreateUser), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
                        {
                            try
                            {
                                var usersList = from c in container.Users
                                                orderby c.Id
                                                select new { c.Id, c.Name, c.Email };
                                Assert.AreEqual(1, usersList.Count()); // there's a root admin user in DB as seed data
                                int savedItemsCount = CreateUser(container);
                                Assert.AreEqual(1, savedItemsCount);
                            }
                            catch (Exception ex)
                            {
                                Assert.Fail(ex.ToString());
                            }
                        }
                    }
                ));
        }
        /// <summary>
        /// Delete user from system (user2)
        /// </summary>
        [TestMethod]
        public void TestDeleteUser()
        {
            DoTest(nameof(TestDeleteUser), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
                        {
                            // prepare testing data - create user1
                            CreateUser(container);
                            Info(string.Format("Before testing, user list items count is: {0}", container.Users.Count().ToString()));
                            var user = container.Users.Where(c => c.Name == "user1").FirstOrDefault() as User;
                            Assert.IsNotNull(user);
                            Assert.AreEqual("user1", user.Name);
                            container.Users.Remove(user);
                            int deletedItemsCount = container.SaveChanges();
                            Assert.AreEqual(1, deletedItemsCount);
                            // check again, nothing in DB
                            int countNumber = container.Users.Count();
                            Info(string.Format("After testing, user list items count is: {0}", countNumber.ToString()));
                            Assert.AreEqual(1, countNumber);
                        }
                    }
                ));
        }

        /// <summary>
        /// Read new user from system (user1)
        /// </summary>
        [TestMethod]
        public void TestReadUser()
        {
            DoTest(nameof(TestReadUser), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
                        {
                            // prepare testing data - create user1
                            CreateUser(container);
                            var usersList = from c in container.Users
                                            orderby c.Id
                                            select new { c.Id, c.Name, c.Email };
                            Assert.AreEqual(2, usersList.Count()); // there's a root admin user in DB as seed data, so here is 2
                            PrintEntity<User>("user1");
                            var users = usersList.ToList();
                            Assert.AreEqual("user1", users[1].Name);
                            Assert.AreEqual("test@advent_test.com", users[1].Email);
                        }
                    }
                ));
        }

        /// <summary>
        /// Update new user from system (user1 -> user2)
        /// </summary>
        [TestMethod]
        public void TestUpdateUser()
        {
            DoTest(nameof(TestUpdateUser), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
                        {
                            // prepare testing data - create user1
                            CreateUser(container);
                            User user = container.Users.Where(c => c.Name == "user1").FirstOrDefault() as User;
                            Assert.IsNotNull(user);
                            Assert.AreEqual("user1", user.Name);
                            user.Name = "user2";
                            int updatedItemsCount = container.SaveChanges();
                            PrintEntity<User>("user2");
                            Assert.AreEqual(1, updatedItemsCount);
                        }
                    }
                ));
        }
        #endregion
    }
}
