using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BookLibDAL.UnitTest
{
    [TestClass]
    public class UnitTestBookLibDALBook : UnitTestBase
    {
        #region Help Methods
        private static void RemoveDirtyData()
        {
            using (BookLibDBContainer container = new BookLibDBContainer())
            {
                container.Histories.RemoveRange(container.Histories);
                container.SaveChanges();
            }

            using (BookLibDBContainer container = new BookLibDBContainer())
            {
                container.Users.RemoveRange(container.Users.Where(x => x.Id > 1).ToList());
                container.SaveChanges();
            }
        }
        #endregion

        #region TearDown for testing method
        /// <summary>
        /// Clean up testing data in db.
        /// </summary>
        [TestCleanup]
        public override void CleanUp()
        {
            RemoveTestingData remove = () => 
            {
                using (BookLibDBContainer container = new BookLibDBContainer())
                {
                    container.Books.RemoveRange(container.Books);
                    container.SaveChanges();
                }
            };

            DoCleanUp(remove);
        }

        /// <summary>
        /// Clean up environment before run test
        /// </summary>
        /// <param name="pTC"></param>
        [TestInitialize]
        public void BeforeTesting()
        {
            RemoveDirtyData();
        }
        #endregion

        #region Test - Book (Test: Create / Read / Update and Delete -> CRUD)
        /// <summary>
        /// Create a new book in system (book1)
        /// </summary>
        [TestMethod]
        public void TestCreateBook()
        {
            StartTest(nameof(TestCreateBook).ToString());

            using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
            {
                try
                {
                    var booksList = from c in container.Books
                                    orderby c.Id
                                    select new { c.Id, c.Name, c.Description };

                    Assert.AreEqual(0, booksList.Count());
                    int savedItemsCount = CreateBook(container);

                    // 2, because one book is created and one history is created
                    Assert.AreEqual(2, savedItemsCount);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.ToString());
                }
            }

            EndTest(nameof(TestCreateBook).ToString());
        }

        /// <summary>
        /// Read new book from system (book1)
        /// </summary>
        //[TestMethod]
        public void TestReadBook()
        {
            StartTest(nameof(TestReadBook).ToString());

            using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
            {

                var booksList = from c in container.Books
                                orderby c.Id
                                select new { c.Id, c.Name, c.Description };

                Assert.AreEqual(1, booksList.Count());

                var books = booksList.ToList();

                Assert.AreEqual("book1", books[0].Name);
                Assert.AreEqual("This is a testing book", books[0].Description);
            }

            EndTest(nameof(TestReadBook).ToString());
        }

        /// <summary>
        /// Update new book from system (book1 -> book2)
        /// </summary>
        //[TestMethod]
        public void TestUpdateBook()
        {
            StartTest(nameof(TestUpdateBook).ToString());

            using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
            {
                Book book = container.Books.Where(c => c.Name == "book1").FirstOrDefault() as Book;

                Assert.IsNotNull(book);
                Assert.AreEqual("book1", book.Name);

                book.Name = "book2";

                int updatedItemsCount = container.SaveChanges();

                Assert.AreEqual(1, updatedItemsCount);
            }

            EndTest(nameof(TestUpdateBook).ToString());
        }

        /// <summary>
        /// Delete book from system (book2)
        /// </summary>
        //[TestMethod]
        public void TestDeleteBook()
        {
            StartTest(nameof(TestDeleteBook).ToString());

            using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
            {
                Book book = container.Books.Where(c => c.Name == "book2").FirstOrDefault() as Book;

                Assert.IsNotNull(book);
                Assert.AreEqual("book2", book.Name);

                container.Books.Remove(book);

                int deletedItemsCount = container.SaveChanges();

                Assert.AreEqual(1, deletedItemsCount);

                // check again, nothing in DB
                var bookLists = (from c in container.Books
                                 orderby c.Id
                                 select new { c.Id, c.Name, c.Description });

                Assert.AreEqual(0, bookLists.Count());
            }

            EndTest(nameof(TestDeleteBook).ToString());
        }
        #endregion
    }
}
