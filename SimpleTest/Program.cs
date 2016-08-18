using BookLibDAL;
using System;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace SimpleTest
{
    public class A
    {
        public string ClassName { get; set; } = nameof(A);

        public string PrintName()
        {
            return ClassName;
        }
    }

    public class B : A
    {
        public B()
        {
            ClassName = nameof(B);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
            {
                try
                {
                    #region Test Read
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
                    #endregion

                    #region Test Write
                    //User newUser = new User() { Name = "user1", Email = "test@advent_test.com", Histories = null };

                    //container.Users.Add(newUser);
                    //int savedItems = container.SaveChanges();
                    #endregion

                    B b = new B();
                    Console.WriteLine(b.PrintName());
                }
                catch (DbUpdateException ex)
                {
                    // ex.InnerException - UpdateException
                    UpdateException upex = ex.InnerException as UpdateException;
                    if (upex != null)
                    {
                        SqlException sqlex = upex.InnerException as SqlException;

                        if (sqlex != null)
                        {
                            Exception e = sqlex.InnerException;

                            if (e != null)
                            {
                                Console.WriteLine(e.Message);
                            }
                            else
                            {
                                Console.WriteLine(sqlex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine(upex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.ReadKey();
        }
    }
}
