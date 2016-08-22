using BookLib.DataAccessLayer;
using BookLib.DataModel;
using BookLib.Delegates;
using Business.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLibDAL.UnitTest
{
    public class UnitTestBase
    {
        #region Hard-code strings
        protected string BookType_English { get; } = "English";
        protected string BookType_Management { get; } = "Management";
        protected string BookType_Program { get; } = "Program";

        protected string Role_Admin { get; } = "Admin";
        protected string Role_User { get; } = "User";

        protected string Status_Lending { get; } = "Lending";
        protected string Status_Ready { get; } = "Ready";
        #endregion

        #region Output logs
        protected void EndTest(string name)
        {
            Log(string.Format("\r\n<<<< {0} - {1} >>>>\r\n", nameof(EndTest).ToString(), name));
        }

        protected void Error(string error)
        {
            Log(string.Format("\r\n**Error**\r\n{0}\r\n", error));
            Assert.Fail(error);
        }

        protected void Info(string info)
        {
            Log(string.Format(">>INFO - {0}", info));
        }

        protected void PrintEntity<T>(string name, string printMessage = "Print entity\r\n{0}")
        {
            using (BookLibDBContainer container = new BookLibDBContainer())
            {
                List<T> items = null;
                switch (typeof(T).Name)
                {
                    case nameof(User):
                        items = Utility.ConvertAnonymousTypeList<T>(container.Users
                                        .Where(x => x.Name == name)
                                        .Select(x => new { x.Id, x.Name, x.Email, x.Role, x.RoleId, x.Histories })) as List<T>;
                        break;
                    case nameof(Book):
                        items = Utility.ConvertAnonymousTypeList<T>(
                                        (
                                            from c in container.Books
                                            orderby c.Id
                                            where c.Name == name
                                            select new
                                            {
                                                c.Id,
                                                c.Name,
                                                c.Description,
                                                c.Status,
                                                c.StatusId,
                                                c.BookType,
                                                c.BookTypeId,
                                                c.Histories
                                            })
                                        ) as List<T>;
                        break;
                }

                Info(string.Format(printMessage, items?.Count == 1 ? items[0].ToString() : string.Empty));
            }
        }

        protected void PrintEntity<T>(T entity, string printMessage = "{0}")
        {
            if (!printMessage.EndsWith("{0}"))
                printMessage += "\r\n{0}";
            Info(string.Format(printMessage, entity?.ToString()));
        }

        protected void PrintEntityList<T>(IEnumerable<object> items)
        {
            List<T> list = Utility.ConvertAnonymousTypeList<T>(items) as List<T>;            
            foreach (var item in list)
                PrintEntity(item, string.Format("Find {0} :\r\n", typeof(T).Name));
        }

        protected void StartTest(string name)
        {
            Log(string.Format("\r\n<<<< {0} - {1} >>>>\r\n", nameof(StartTest).ToString(), name));
        }

        protected void Warn(string warning)
        {
            Log(string.Format("\r\n==Warning - {0}\r\n", warning));
        }

        private void Log(string log)
        {
            Console.WriteLine(log);
        }
        #endregion

        #region Help Methods        
        // book1, This is a testing book, Ready, Program
        // history(1): 2016-08-01, 2016-08-31, user1, test@advent_test.com
        protected int CreateBook(BookLibDBContainer container)
        {
            CreateUser(container);

            Status status = GetStatus(Status_Ready);
            BookType bookType = GetBookType(BookType_Program);
            Book newBook = new Book()
            {
                Name = "book1",
                Description = "This is a testing book",
                StatusId = status.Id,
                BookTypeId = bookType.Id,
                Histories = new List<History>() {
                    new History()
                    {
                        StartTime = Convert.ToDateTime("2016-08-01"),
                        ReturnTime = Convert.ToDateTime("2016-08-31"),
                        UserId = container.Users.Where(x => x.Name == "user1").FirstOrDefault().Id
                    }
                }
            };
            container.Books.Add(newBook);
            int savedItemsCount = container.SaveChanges();
            PrintEntity<Book>("book1", "Create testing book:\r\n{0}");
            return savedItemsCount;
        }

        // user1, test@advent_test.com
        protected int CreateUser(BookLibDBContainer container)
        {
            try
            {
                Role role = GetRole(Role_User);
                User newUser = new User() { Name = "user1", Email = "test@advent_test.com", RoleId = role.Id };
                container.Users.Add(newUser);
                int savedItemsCount = container.SaveChanges();
                PrintEntity<User>("user1", "Create testing user:\r\n{0}");
                return savedItemsCount;
            }
            catch (Exception ex)
            {
                Error(ex.ToString());
                return 0;
            }
        }

        // Get BookType object by name
        protected BookType GetBookType(string name)
        {
            using (BookLibDBContainer container = new BookLibDBContainer())
            {
                BookType bookType = container.BookTypes.Where(x => x.Name == name).FirstOrDefault() as BookType;

                if (null != bookType)
                {
                    return bookType;
                }
                else
                {
                    return null;
                }
            }
        }

        // Get Role object by name
        protected Role GetRole(string name)
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

        // Get Status object by name
        protected Status GetStatus(string name)
        {
            using (BookLibDBContainer container = new BookLibDBContainer())
            {
                Status status = container.Status.Where(x => x.Name == name).FirstOrDefault() as Status;

                if (null != status)
                {
                    return status;
                }
                else
                {
                    return null;
                }
            }
        }

        protected void DoTest(string methodName, Delegate realTesting)
        {
            StartTest(methodName);
            if (null == realTesting)
            {
                realTesting = new NoneParaDelegateMethod
                    (
                        () => { Warn("Need to implement this testing methd."); }
                    );
            }
            realTesting.DynamicInvoke();
            EndTest(methodName);
        }
        #endregion

        #region CleanUp
        public virtual void CleanUp()
        {
            DoCleanUp(new NoneParaDelegateMethod
                (
                    () =>
                    {
                        Warn("Need to clean up testing data in this methd.");
                    }
                ));
        }

        protected void DoCleanUp(Delegate cleanup)
        {
            try
            {
                if (null != cleanup)
                {
                    cleanup.DynamicInvoke();
                }
            }
            catch (Exception ex)
            {
                Error(ex.ToString());
            }

            Log("[[ CleanUp ]]");
        }
        #endregion
    }
}
