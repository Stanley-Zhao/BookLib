using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BookLibDAL.UnitTest
{
    [TestClass]
    public class UnitTestBookLibDALCommon : UnitTestBase
    {
        #region Test - Status (Test: Read)
        /// <summary>
        /// We only need read, this is a "ready-only" data in system
        /// </summary>
        [TestMethod]
        public void TestReadStatus()
        {
            DoTest(nameof(TestReadStatus), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
                        {
                            var statuses = (from c in container.Status
                                            orderby c.Name
                                            select new { c.Id, c.Name });
                            Assert.AreEqual(2, statuses.Count());
                            PrintEntityList<Status>(statuses); // print log
                            var list = statuses.ToList();
                            Assert.AreEqual("Lending", list[0].Name);
                            Assert.AreEqual("Ready", list[1].Name);
                        }
                    }
                ));
        }
        #endregion

        #region Test - BookType (Test: Read)
        /// <summary>
        /// We only need read, this is a "ready-only" data in system
        /// </summary>
        [TestMethod]
        public void TestReadBookType()
        {
            DoTest(nameof(TestReadBookType), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
                        {
                            var bookTypes = (from c in container.BookTypes
                                                orderby c.Name
                                                select new { c.Id, c.Name });
                            Assert.AreEqual(3, bookTypes.Count());
                            PrintEntityList<BookType>(bookTypes); // print log
                            var list = bookTypes.ToList();
                            Assert.AreEqual("English", list[0].Name);
                            Assert.AreEqual("Management", list[1].Name);
                            Assert.AreEqual("Program", list[2].Name);
                        }
                    }
                ));
        }
        #endregion

        #region Test - Role (Test: Read)
        /// <summary>
		/// We only need read, this is a "ready-only" data in system
		/// </summary>
		[TestMethod]
        public void TestReadRole()
        {
            DoTest(nameof(TestReadRole), new NoneParaDelegateMethod
                (
                    () =>
                    {
                        using (BookLibDBContainer container = new BookLibDBContainer())
                        {
                            var roles = (from c in container.Roles
                                         orderby c.Name
                                         select new { c.Id, c.Name });
                            Assert.AreEqual(2, roles.Count());
                            PrintEntityList<Role>(roles); // print log
                            Assert.AreEqual("Admin", roles.ToList()[0].Name);
                            Assert.AreEqual("User", roles.ToList()[1].Name);
                        }
                    }
                ));
        }
        #endregion
    }
}
