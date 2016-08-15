using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BookLibDAL.UnitTest
{
	[TestClass]
	public class UnitTestBookLibDAL
	{
		private static TestContext tc = null;

		[ClassInitialize]
		public static void TestStartUp(TestContext pTestContext)
		{
			tc = pTestContext;
			tc.WriteLine("Start UnitTestBookLibDAL");
		}

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
	}
}
