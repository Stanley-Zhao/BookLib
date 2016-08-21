using BookLib.DataAccessLayer;
using BookLib.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace SimpleTest
{
    class Program
    {
        private static Role GetRole(string name)
        {
            using (BookLibDBContainer container = new BookLibDBContainer())
            {
                Role role = container.Roles.Where(x => x.Name == name).FirstOrDefault() as Role;

                if (null != role)
                {
                    return role;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Converts a list of objects into a list of strongly typed objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="anonymouslyTypedList"></param>
        /// <returns></returns>
        public static IEnumerable<T> ConvertAnonymousType<T>(IEnumerable<object> list)
        {
            var stronglyTypedList = (List<T>)Activator.CreateInstance(typeof(List<T>), null);

            foreach (var item in list)
            {
                var obj = (T)Activator.CreateInstance(typeof(T), null);
                foreach (System.Reflection.PropertyInfo pi in typeof(T).GetProperties())
                {
                    var value = item.GetType().GetProperty(pi.Name).GetValue(item);
                    typeof(T).GetProperty(pi.Name).SetValue(obj, value, null);
                }
                stronglyTypedList.Add(obj);
            }

            return stronglyTypedList;
        }

        static void Main(string[] args)
        {
            using (BookLibDBContainer container = new BookLibDBContainer())
            {
                try
                {
                    #region Test Read
                    //var bookList = container.Books.ToList();

                    ////string sql = "select [Name] from Status";
                    ////var objList2 = container.Database.SqlQuery<string>(sql);
                    //foreach (var item in bookList)
                    //{
                    //    Console.WriteLine(item.ToString());
                    //}

                    //Console.WriteLine(bookList.Count());

                    //var userList = container.Users.Where(x => x.Id > 1).ToList();
                    //foreach (var item2 in userList)
                    //{
                    //    Console.WriteLine(item2.ToString());
                    //}
                    #endregion

                    #region Test Write
                    Role role = GetRole("User");
                    string email = string.Format("{0}@advent_test.com", DateTime.Now.Ticks.ToString());
                    User newUser = new User() { Name = "user1", Email = email,
                        RoleId = role.Id };

                    container.Users.Add(newUser);
                    int savedItemsCount = container.SaveChanges();
                                       
                    var users = (from c in container.Users
                                        orderby c.Id
                                        where c.Email == email
                                        select new { c.Id, c.Name, c.Email, c.RoleId, c.Role, c.Histories }).ToArray();

                    List<User> userList = ConvertAnonymousType<User>(users) as List<User>;

                    Console.WriteLine(string.Format("Create testing user:\r\n{0}", userList?.Count==1? userList[0].ToString():string.Empty));
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
