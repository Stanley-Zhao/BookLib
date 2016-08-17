using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BookLibDAL.UnitTest
{
	[TestClass]
	public class UnitTestBookLibDALCommon
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
			Console.WriteLine("Start UnitTestBookLibCommon");
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

        #region Test - Status (Test: Read)
        /// <summary>
        /// We only need read, this is a "ready-only" data in system
        /// </summary>
        [TestMethod]
		public void TestReadStatus()
		{
			Console.WriteLine("Run TestReadStatus()");

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

			Console.WriteLine("Done of TestReadStatus()");
		}
		#endregion

		#region Test - BookType (Test: Read)
		/// <summary>
		/// We only need read, this is a "ready-only" data in system
		/// </summary>
		[TestMethod]
		public void TestReadBookType()
		{
			Console.WriteLine("Run TestReadBookType()");

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

			Console.WriteLine("Done of TestReadBookType()");
		}
		#endregion
	}
}
