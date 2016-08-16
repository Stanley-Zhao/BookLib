using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BookLibDAL.UnitTest
{
	[TestClass]
	public class UnitTestBookLibDAL
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
			tc.WriteLine("Start UnitTestBookLibDAL");
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

		#region Test - User (Test: Create / Read / Update and Delete -> CRUD)
		/// <summary>
		/// Create a new user in system (user1)
		/// </summary>
		[TestMethod]
		public void TestCreateUser()
		{
			Console.WriteLine("Run TestCreateUser()");

			using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
			{
				try
				{
					//var usersList = from c in container.Users
					//				orderby c.Id
					//				select new { c.Id, c.Name, c.Email };

					//Assert.AreEqual(0, usersList.Count());

					User newUser = new User() { Name = "user1", Email = "test@advent_test.com", Histories = null };

					container.Users.Add(newUser);
					int savedItems = container.SaveChanges();

					Assert.AreEqual(1, savedItems);
				}
				catch (Exception ex)
				{
					Assert.Fail(ex.ToString());
				}
			}

			Console.WriteLine("Done of TestCreateUser()");
		}

		/// <summary>
		/// Read new user from system (user1)
		/// </summary>
		[TestMethod]
		public void TestReadUser()
		{
			Console.WriteLine("Run TestReadUser()");

			using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
			{

				var usersList = from c in container.Users
								orderby c.Id
								select new { c.Id, c.Name, c.Email };

				Assert.AreEqual(1, usersList.Count());

				var users = usersList.ToList();

				Assert.AreEqual("user1", users[0].Name);
				Assert.AreEqual("test@advent_test.com", users[0].Email);
			}

			Console.WriteLine("Run TestReadUser()");
		}

		/// <summary>
		/// Update new user from system (user1 -> user2)
		/// </summary>
		[TestMethod]
		public void TestUpdateUser()
		{
			Console.WriteLine("Run TestUpdateUser()");

			using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
			{
				//Assert.AreEqual("TODO", TODO);
				Assert.Fail("Not implement");
			}

			Console.WriteLine("Done of TestUpdateUser()");
		}

		/// <summary>
		/// Delete user from system (user2)
		/// </summary>
		[TestMethod]
		public void TestDeleteUser()
		{
			Console.WriteLine("Run TestDeleteUser()");

			using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
			{
				//Assert.AreEqual("TODO", TODO);
				Assert.Fail("Not implement");
			}

			Console.WriteLine("Done of TestDeleteUser()");
		}
		#endregion
	}
}
