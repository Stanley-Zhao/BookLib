using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BookLibDAL.UnitTest
{
    [TestClass]
    public class UnitTestBookLibDALHistory : UnitTestBase
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
                            container.Books.RemoveRange(container.Books);
                            container.Users.RemoveRange(container.Users.Where(x => x.Id > 1).ToList()); // Id=1, the root admin user
                            container.SaveChanges();
                        }
                    }
                ));
        }
        #endregion

        #region Test - History (Test: Create / Delete / Read and Update -> CRUD)
        /// <summary>
        /// Create a new history in system (user1, book1, 2016-09-01 to 2016-09-30)
        /// </summary>
        [TestMethod]
        public void TestCreateHistory()
        {
            DoTest(nameof(TestCreateHistory), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
                        {
                            try
                            {
                                CreateBook(container);
                                User user = container.Users.Where(x => x.Id == 1).FirstOrDefault() as User;
                                Book book = container.Books.Where(x => x.Name == "book1").FirstOrDefault() as Book;
                                History history = new History()
                                {
                                    BookId = book.Id,
                                    UserId = user.Id,
                                    StartTime = Convert.ToDateTime("2016-09-01"),
                                    ReturnTime = Convert.ToDateTime("2016-09-30")
                                };
                                container.Histories.Add(history);
                                int savedItemsCount = container.SaveChanges();
                                Assert.AreEqual(1, savedItemsCount);
                                PrintEntity<Book>("book1"); // print log for histories
                                Assert.AreEqual(2, container.Histories.Count());
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
        //[TestMethod]
        public void TestDeleteHistory()
        {
            DoTest(nameof(TestDeleteHistory), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
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
        //[TestMethod]
        public void TestReadHistory()
        {
            DoTest(nameof(TestReadHistory), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
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
        //[TestMethod]
        public void TestUpdateHistory()
        {
            DoTest(nameof(TestUpdateHistory), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
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
