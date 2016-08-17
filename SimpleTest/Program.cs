using BookLibDAL;
using System;
using System.Linq;

namespace SimpleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
			{
                //var statusList = from c in container.Status								  
                //				  orderby c.Name
                //				  select new { c.Id, c.Name};

                ////string sql = "select [Name] from Status";
                ////var objList2 = container.Database.SqlQuery<string>(sql);
                //foreach (var item in statusList)
                //{
                //	Console.WriteLine(item);
                //}

                //Console.WriteLine(statusList.Count());

                User newUser = new User() { Name = "user1", Email = "test@advent_test.com", Histories = null };

                container.Users.Add(newUser);
                int savedItems = container.SaveChanges();
            }

			Console.ReadKey();
		}
	}
}
