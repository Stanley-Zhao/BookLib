using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BookLibDAL.UnitTest
{
    [TestClass]
    public class UnitTestBookLibDALBook : UnitTestBase
    {
        #region Help Methods
        private Status GetStatus(string name)
        {
            using (BookLibDBContainer container = new BookLibDBContainer())
            {
                Status status = container.Status.Where(x => x.Name == name).FirstOrDefault() as Status;

                if (null != status)
                {
                    return status;
                }
                else
                {
                    return null;
                }
            }
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

                    Book newBook = new Book() { Name = "book1", Description = "This is a testing book", Status = , Histories = null };

                    container.Books.Add(newBook);
                    int savedItemsCount = container.SaveChanges();

                    Assert.AreEqual(1, savedItemsCount);
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
        [TestMethod]
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
        [TestMethod]
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
        [TestMethod]
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
