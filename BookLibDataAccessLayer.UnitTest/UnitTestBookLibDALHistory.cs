using BookLib.DataAccessLayer;
using BookLib.DataModel;
using BookLib.Delegates;
using Business.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        /// Create a new book lending history in system (user1, book1, 2016-09-01 to 2016-09-30)
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
        /// Delete book lending history from system (user2)
        /// </summary>
        [TestMethod]
        public void TestDeleteHistory()
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
                                User user = container.Users.Where(x => x.Name == "user1").FirstOrDefault() as User;
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
                                var history1 = (from h in container.Histories
                                                join b in container.Books on h.BookId equals b.Id
                                                join u in container.Users on h.UserId equals u.Id
                                                where b.Name == "book1" && u.Name == "user1"
                                                select new
                                                {
                                                    h.Id,
                                                    h.BookId,
                                                    h.Book,
                                                    h.Book.BookType,
                                                    h.Book.Status,
                                                    h.UserId,
                                                    h.User,
                                                    h.User.Role,
                                                    h.StartTime,
                                                    h.ReturnTime
                                                }).FirstOrDefault();
                                History book1History = Utility.ConvertAnonymousType<History>(history1); // book1History is strong type which can print log
                                PrintEntity(book1History, string.Format("Find {0} entity:", nameof(History))); // print log
                                var historyToBeDeleted = container.Histories.Where(x => x.Id == book1History.Id).FirstOrDefault();
                                container.Histories.Remove(historyToBeDeleted);
                                container.SaveChanges();
                                PrintEntity<Book>("book1"); // print log for histories
                                Assert.AreEqual(1, container.Histories.Count());
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
        /// Read new book lending history from system (user1)
        /// </summary>
        [TestMethod]
        public void TestReadHistory()
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
                                var history1 = (from h in container.Histories
                                               join b in container.Books on h.BookId equals b.Id
                                               where b.Name == "book1"
                                               select new
                                               {
                                                   h.Id,
                                                   h.BookId,
                                                   h.Book,
                                                   h.Book.BookType,
                                                   h.Book.Status,
                                                   h.UserId,
                                                   h.User,
                                                   h.User.Role,
                                                   h.StartTime,
                                                   h.ReturnTime
                                               }).FirstOrDefault();
                                History history = Utility.ConvertAnonymousType<History>(history1);
                                PrintEntity(history, string.Format("Find {0} entity:", nameof(History))); // print log
                                Assert.AreEqual(1, container.Histories.Count());
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
        /// Update new book lending history from system (user1 -> [root admin] user)
        /// </summary>
        [TestMethod]
        public void TestUpdateHistory()
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
                                var history1 = (from h in container.Histories
                                                join b in container.Books on h.BookId equals b.Id
                                                where b.Name == "book1"
                                                select new
                                                {
                                                    h.Id,
                                                    h.BookId,
                                                    h.Book,
                                                    h.Book.BookType,
                                                    h.Book.Status,
                                                    h.UserId,
                                                    h.User,
                                                    h.User.Role,
                                                    h.StartTime,
                                                    h.ReturnTime
                                                }).FirstOrDefault();
                                History book1History = Utility.ConvertAnonymousType<History>(history1);
                                PrintEntity(book1History, string.Format("Find {0} entity:", nameof(History))); // print log
                                book1History.UserId = 1; // root admin user
                                container.SaveChanges();
                                var history2 = (from h in container.Histories
                                                join b in container.Books on h.BookId equals b.Id
                                                where b.Name == "book1"
                                                select new
                                                {
                                                    h.Id,
                                                    h.BookId,
                                                    h.Book,
                                                    h.Book.BookType,
                                                    h.Book.Status,
                                                    h.UserId,
                                                    h.User,
                                                    h.User.Role,
                                                    h.StartTime,
                                                    h.ReturnTime
                                                }).FirstOrDefault();
                                History book1History2 = Utility.ConvertAnonymousType<History>(history2);
                                PrintEntity(book1History2, string.Format("Find {0} entity:", nameof(History))); // print log
                                Book book1 = container.Books.Where(x => x.Name == "book1").FirstOrDefault() as Book;
                                Assert.AreEqual(book1.Id, book1History2.BookId);
                                PrintEntity<Book>("book1"); // print log for histories
                            }
                            catch (Exception ex)
                            {
                                Assert.Fail(ex.ToString());
                            }
                        }
                    }
                ));
        }
        #endregion
    }
}
