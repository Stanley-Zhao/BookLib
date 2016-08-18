using BookLibDAL;
using System;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace SimpleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (BookLibDAL.BookLibDBContainer container = new BookLibDAL.BookLibDBContainer())
            {
                try
                {
                    #region Test Read
                    var bookList = container.Books.ToList();

                    //string sql = "select [Name] from Status";
                    //var objList2 = container.Database.SqlQuery<string>(sql);
                    foreach (var item in bookList)
                    {
                        Console.WriteLine(item.ToString());
                    }

                    Console.WriteLine(bookList.Count());
                    #endregion

                    #region Test Write
                    //User newUser = new User() { Name = "user1", Email = "test@advent_test.com", Histories = null };

                    //container.Users.Add(newUser);
                    //int savedItems = container.SaveChanges();
                    #endregion

                    #region Test Update
                    #endregion
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
