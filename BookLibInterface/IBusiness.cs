using BookLib.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib.Interface
{
    public interface IBusiness
    {
        #region Book Types and Status
        IReturnValue<BookType> GetAllBookTypes();
        IReturnValue<Status> GetAllStatus();
        #endregion               

        #region Uers
        IReturnValue<User> GetAllUsers();
        IReturnValue<User> GetUserByEmail(string email);
        IReturnValue<User> GetUserById(int id);
        IReturnValue<User> GetUserByName(string name);
        IReturnValue<User> UpdateUserName(int userId, string name, string email);
        #endregion

        #region Book
        IReturnValue<Book> AddANewBook(string name, string description, int bookTypeId);
        IReturnValue<Book> BorrowABook(string email, string bookName, DateTime startTime);
        IReturnValue<Book> DeleteABook(string name);
        IReturnValue<Book> GetAllBooks();
        IReturnValue<Book> GetBookById(int id);
        IReturnValue<Book> GetBookByName(string name);
        IReturnValue<Book> GetBooksByBookType(int bookTypeId);
        IReturnValue<Book> GetBooksByStatus(int statusId);
        IReturnValue<Book> ReturnABook(string bookName, DateTime returnTime);
        IReturnValue<Book> SearchBookByDescription(string keywords); // search by include key words
        IReturnValue<Book> SearchBookByName(string startKeywords); // search by start with key words
        IReturnValue<Book> UpdateBookInfo(int bookId, string name, string description, int bookTypeId, int statusId);
        #endregion

        #region History
        IReturnValue<History> GetAllHistories();
        IReturnValue<History> GetHistoriesByBookName(string bookName);
        IReturnValue<History> GetHistoriesByBookName(string bookName, DateTime startTime, DateTime returnTime);
        IReturnValue<History> GetHistoriesByUserEmail(string email);
        IReturnValue<History> GetHistoriesByUserEmail(string email, DateTime startTime, DateTime returnTime);
        #endregion
    }
}
