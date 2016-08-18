using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            StartTest(nameof(TestReadStatus).ToString());

            using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
			{
				var statusList = from c in container.Status
								 orderby c.Name
								 select new { c.Id, c.Name };

				Assert.AreEqual(2, statusList.Count());

				var statuses = statusList.ToList();

				Assert.AreEqual("Lending", statuses[0].Name);
				Assert.AreEqual("Ready", statuses[1].Name);
			}

            EndTest(nameof(TestReadStatus).ToString());
        }
		#endregion

		#region Test - BookType (Test: Read)
		/// <summary>
		/// We only need read, this is a "ready-only" data in system
		/// </summary>
		[TestMethod]
		public void TestReadBookType()
		{
            StartTest(nameof(TestReadBookType).ToString());

            using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
			{
				var bookTypeList = from c in container.BookTypes
								 orderby c.Name
								 select new { c.Id, c.Name };

				Assert.AreEqual(3, bookTypeList.Count());

				var bookTypes = bookTypeList.ToList();

				Assert.AreEqual("English", bookTypes[0].Name);
				Assert.AreEqual("Management", bookTypes[1].Name);
				Assert.AreEqual("Program", bookTypes[2].Name);
			}

            EndTest(nameof(TestReadBookType).ToString());
        }
		#endregion
	}
}
