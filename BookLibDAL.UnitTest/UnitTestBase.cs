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

        // user1, test@advent_test.com
        protected int CreateUser(BookLibDBContainer container)
        {
            try
            {
                User newUser = new User() { Name = "user1", Email = "test@advent_test.com" };

                container.Users.Add(newUser);
                int savedItemsCount = container.SaveChanges();
                Info("create testing user - user1");
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

            Book newBook = new Book()
            {
                Name = "book1",
                Description = "This is a testing book",
                StatusId = GetStatus(Status_Ready).Id,
                BookTypeId = GetBookType(BookType_Program).Id,
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
            Info("create testing book - book1");
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
#if !DEBUG
                    cleanup.DynamicInvoke();
#endif
                    throw new Exception("testing");
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
