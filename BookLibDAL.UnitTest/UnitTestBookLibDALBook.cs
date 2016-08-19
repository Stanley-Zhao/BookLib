using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace BookLibDAL.UnitTest
{
    [TestClass]
    public class UnitTestBookLibDALBook : UnitTestBase
    {
        #region Setup and CleanUp for testing method
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
                            container.Users.RemoveRange(container.Users.Where(x => x.Id > 1));
                            container.SaveChanges();
                        }
                    }
                ));
        }
        #endregion

        #region Test - Book (Test: Create / Delete / Read and Update -> CRUD)
        /// <summary>
        /// Create a new book in system (book1)
        /// </summary>
        [TestMethod]
        public void TestCreateBook()
        {
            DoTest(nameof(TestCreateBook), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
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
                    }
                ));
        }

        /// <summary>
        /// Create a duplicate book in system (book1), this time, there will be an error from DB say user email is not allowed to be duplicated.
        /// </summary>
        [TestMethod]
        public void TestCreateDuplicateBook()
        {
            DoTest(nameof(TestCreateDuplicateBook), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
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
                                Book newBook = new Book()
                                {
                                    Name = "book1",
                                    Description = "This is a testing data",
                                    StatusId = GetStatus(Status_Ready).Id,
                                    BookTypeId = GetBookType(BookType_Program).Id,
                                    Histories = null
                                };
                                container.Books.Add(newBook);
                                savedItemsCount = container.SaveChanges();
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
        /// Delete book from system (book1)
        /// </summary>
        [TestMethod]
        public void TestDeleteBook()
        {
            DoTest(nameof(TestDeleteBook), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
                        {
                            CreateBook(container);
                            Info(string.Format("Before testing, book list items count is: {0}", container.Books.Count().ToString()));
                            Book book = container.Books.Where(c => c.Name == "book1").FirstOrDefault() as Book;
                            Assert.IsNotNull(book);
                            Assert.AreEqual("book1", book.Name);
                            container.Histories.RemoveRange(container.Histories.Where(x => x.BookId == book.Id));
                            container.Books.Remove(book);
                            int deletedItemsCount = container.SaveChanges();
                            Assert.AreEqual(2, deletedItemsCount); // one is for history record and one is for book record
                            int countNumber = container.Books.Count();
                            Info(string.Format("After testing, book lists item count is: {0}", countNumber.ToString()));
                            Assert.AreEqual(0, countNumber);
                        }
                    }
                ));
        }

        /// <summary>
        /// Read new book from system (book1)
        /// </summary>
        [TestMethod]
        public void TestReadBook()
        {
            DoTest(nameof(TestReadBook), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
                        {
                            CreateBook(container);
                            var booksList = from c in container.Books
                                            orderby c.Id
                                            select new { c.Id, c.Name, c.Description };
                            PrintEntity<Book>("book1"); // print log
                            Assert.AreEqual(1, booksList.Count());
                            var books = booksList.ToList();
                            Assert.AreEqual("book1", books[0].Name);
                            Assert.AreEqual("This is a testing book", books[0].Description);
                        }
                    }
                ));
        }

        /// <summary>
        /// Update new book from system (book1 -> book2)
        /// </summary>
        [TestMethod]
        public void TestUpdateBook()
        {
            DoTest(nameof(TestUpdateBook), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
                        {
                            CreateBook(container);
                            Book book = container.Books.Where(c => c.Name == "book1").FirstOrDefault() as Book;
                            PrintEntity<Book>("book1"); // print log
                            Assert.IsNotNull(book);
                            Assert.AreEqual("book1", book.Name);
                            book.Name = "book2";
                            int updatedItemsCount = container.SaveChanges();
                            Assert.AreEqual(1, updatedItemsCount);
                            PrintEntity<Book>("book2"); // print log
                        }
                    }
                ));
        }
        #endregion
    }
}
