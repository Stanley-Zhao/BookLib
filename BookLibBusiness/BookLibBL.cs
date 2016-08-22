using BookLib.DataAccessLayer;
using BookLib.DataModel;
using BookLib.Interface;
using Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLib.Business
{
    public class BookLibBL : IBusiness
    {
        #region Singleton
        private BookLibBL() { }

        public static BookLibBL Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            internal static readonly BookLibBL instance = new BookLibBL();

            static Nested()
            {
            }
        }
        #endregion

        #region Status and BookTypes
        public IReturnValue<BookType> GetAllBookTypes()
        {
            throw new NotImplementedException();
        }

        public IReturnValue<Status> GetAllStatus()
        {
            IReturnValue<Status> returnValue = new ReturnValue<Status>();

            using (BookLibDBContainer container = new BookLibDBContainer())
            {
                var status = (from s in container.Status
                              select new { s.Id, s.Name });
                List<Status> list = Utility.ConvertAnonymousTypeList<Status>(status) as List<Status>;
                returnValue.SingleEntity = false;
                returnValue.ReturnEntities = list.ToArray();
            }

            return returnValue;            
        }
        #endregion

        #region Users
        public IReturnValue<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public IReturnValue<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<User> GetUserByName(string name)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<User> UpdateUserName(int userId, string name, string email)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Books
        public IReturnValue<Book> AddANewBook(string name, string description, int bookTypeId)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<Book> BorrowABook(string email, string bookName, DateTime startTime)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<Book> DeleteABook(string name)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public IReturnValue<Book> GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<Book> GetBookByName(string name)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<Book> GetBooksByBookType(int bookTypeId)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<Book> GetBooksByStatus(int statusId)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<Book> ReturnABook(string bookName, DateTime returnTime)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<Book> SearchBookByDescription(string keywords)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<Book> SearchBookByName(string startKeywords)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<Book> UpdateBookInfo(int bookId, string name, string description, int bookTypeId, int statusId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Histories
        public IReturnValue<History> GetAllHistories()
        {
            throw new NotImplementedException();
        }

        public IReturnValue<History> GetHistoriesByBookName(string bookName)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<History> GetHistoriesByBookName(string bookName, DateTime startTime, DateTime returnTime)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<History> GetHistoriesByUserEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<History> GetHistoriesByUserEmail(string email, DateTime startTime, DateTime returnTime)
        {
            throw new NotImplementedException();
        }

        public IReturnValue<History> UpdateHistory()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
