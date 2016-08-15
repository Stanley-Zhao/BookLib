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
				var statusList = from c in container.Status								  
								  orderby c.Name
								  select new { c.Id, c.Name};
								
				//string sql = "select [Name] from Status";
				//var objList2 = container.Database.SqlQuery<string>(sql);
				foreach (var item in statusList)
				{
					Console.WriteLine(item);
				}

				Console.WriteLine(statusList.Count());
			}

			Console.ReadKey();
		}
	}
}
