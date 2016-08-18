using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibDAL.UnitTest
{
    public class UnitTestBase
    {
        #region Hard-code strings
        protected string Role_Admin { get; } = "Admin";
        protected string Role_User { get; } = "User";

        protected string Status_Lending { get; } = "Lending";
        protected string Status_Ready { get; } = "Ready";

        protected string BookType_English { get; } = "English";
        protected string BookType_Management { get; } = "Management";
        protected string BookType_Program { get; } = "Program";
        #endregion

        #region Output logs
        protected void StartTest(string name)
        {
            Log(string.Format("{0} - {1}", nameof(StartTest).ToString(), name));
        }

        protected void EndTest(string name)
        {
            Log(string.Format("{0} - {1}", nameof(EndTest).ToString(), name));
        }

        protected void Info(string info)
        {
            Log(string.Format("  INFO - {0}", info));
        }

        protected void Error(string error)
        {
            Log(string.Format("\r\n** Error - {0}\r\n", error));
            Assert.Fail(error);
        }

        private void Log(string log)
        {
            Console.WriteLine(log);
        }
        #endregion

        #region Help Methods
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

        // user1, test@advent_test.com
        protected int CreateUser(BookLibDBContainer container)
        {
            try
            {
                Role role = GetRole(Role_User);
                User newUser = new User() { Name = "user1", Email = "test@advent_test.com", RoleId = role.Id};

                container.Users.Add(newUser);
                int savedItemsCount = container.SaveChanges();

                User savedUser = container.Users.Where(x => x.Name == "user1").FirstOrDefault() as User;

                Info(string.Format("Create testing user:\r\n{0}", savedUser.ToString()));
                return savedItemsCount;
            }
            catch(Exception ex)
            {
                Error(ex.ToString());
                return 0;
            }
        }

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
                Histories = new List<BookLibDAL.History>() {
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

            Book savedBook = container.Books.Where(x => x.Name == "book1").FirstOrDefault() as Book;

            Info(string.Format("Create testing book:\r\n{0}", savedBook.ToString()));
            return savedItemsCount;
        }
        #endregion

        #region Teardown
        protected delegate void RemoveTestingData();

        public virtual void CleanUp()
        {
            RemoveTestingData remove = () => { Console.WriteLine("Need to clean up testing data in this methd."); };
            DoCleanUp(remove);
        }

        protected void DoCleanUp(Delegate cleanup)
        {
            try
            {
                if (null != cleanup)
                {
//#if !DEBUG
                    // remove all histories first, other wise we cannot remove users or books because of FK
                    using (BookLibDBContainer container = new BookLibDBContainer())
                    {
                        container.Histories.RemoveRange(container.Histories);
                        container.SaveChanges();
                    }

                    cleanup.DynamicInvoke();
//#endif
                    Info("Teardown - cleanup");
                }
            }
            catch (Exception ex)
            {
                Error(ex.ToString());
            }
        }
#endregion
    }
}
