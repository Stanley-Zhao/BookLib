using BookLib.Common;
using BookLib.DataAccessLayer;
using BookLib.DataModel;
using BookLib.Delegates;
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

        #region Helping Methods
        private IReturnValue<T> DoTask<T>(DoBusinessTaskDelegateMethod<T> method, string methodName)
        {
            IReturnValue<T> returnValue = new ReturnValue<T>();

            try
            {
                if (method == null)
                    method = new DoBusinessTaskDelegateMethod<T>
                        (
                            (r) =>
                            {
                                throw new NotImplementedException(methodName); // do real work here
                            }
                        );

                returnValue.StatusCode = (int)method.DynamicInvoke();

                if(returnValue.StatusCode != BookLibStatusCode.STATUS_OK) // something wrong, get expected error message
                {                    
                    returnValue.Message = methodName + ":" + BookLibStatusCode.GetStatusMessage(returnValue.StatusCode);
                    returnValue.DetailMessage = methodName + Environment.NewLine + BookLibStatusCode.GetStatusMessage(returnValue.StatusCode);
                }
            }
            catch (Exception ex)
            {
                returnValue.HasError = true;
                returnValue.StatusCode = BookLibStatusCode.STATUS_ERROR_UNKNOWN;
                returnValue.DetailMessage = methodName + Environment.NewLine + ex.ToString();
                returnValue.Message = methodName + ":" + BookLibStatusCode.GetStatusMessage(BookLibStatusCode.STATUS_ERROR_UNKNOWN);
            }

            return returnValue;
        }

        private int HandelSingleEntity<T>(ref IReturnValue<T> r, object objs)
        {
            r.SingleEntity = true;
            if (null != objs)
            {
                T user = Utility.ConvertAnonymousType<T>(objs);
                r.ReturnEntity = user;
                return BookLibStatusCode.STATUS_OK;
            }
            else
            {
                r.ReturnEntity = default(T);
                return BookLibStatusCode.STATUS_ERROR_NOT_FOUND;
            }
        }

        private int HandleEntitiesList<T>(ref IReturnValue<T> r, object objs, bool allowEmptyList = true)
        {
            r.SingleEntity = false;
            List<T> objects = null;

            if (null != objs)            
                objects = Utility.ConvertAnonymousTypeList<T>(objs as IEnumerable<object>) as List<T>;            

            if (null == objects && allowEmptyList)
            {
                r.ReturnEntities = null;
                r.Message = BookLibStatusCode.GetStatusMessage(BookLibStatusCode.STATUS_OK);
                return BookLibStatusCode.STATUS_OK;
            }
            else if(null != objects)
            {
                r.ReturnEntities = objects.ToArray();
                r.AffectEntitiesCount = objects.Count();
                r.Message = BookLibStatusCode.GetStatusMessage(BookLibStatusCode.STATUS_OK);
                return BookLibStatusCode.STATUS_OK;
            }
            else
            {
                r.ReturnEntities = null;
                r.AffectEntitiesCount = 0;
                r.Message = BookLibStatusCode.GetStatusMessage(BookLibStatusCode.STATUS_ERROR_EMPTY_LIST);
                return BookLibStatusCode.STATUS_ERROR_EMPTY_LIST;
            }
        }
        #endregion

        #region Status and BookTypes
        public IReturnValue<BookType> GetAllBookTypes()
        {
            return DoTask<BookType>
                (
                    new DoBusinessTaskDelegateMethod<BookType>(
                        (r) =>
                        {
                            using (BookLibDBContainer container = new BookLibDBContainer())
                            {
                                var bookTypes = (from b in container.BookTypes
                                                 select new { b.Id, b.Name });

                                return HandleEntitiesList<BookType>(ref r, bookTypes, false);                                
                            }
                        }
                ), nameof(GetAllBookTypes));
        }

        public IReturnValue<Status> GetAllStatus()
        {
            return DoTask<Status>
                (
                    new DoBusinessTaskDelegateMethod<Status>(
                        (r) =>
                        {
                            using (BookLibDBContainer container = new BookLibDBContainer())
                            {
                                var status = (from s in container.Status
                                              select new { s.Id, s.Name });

                                return HandleEntitiesList<Status>(ref r, status, false);
                            }
                        }
                ), nameof(GetAllStatus));
        }
        #endregion

        #region Users
        public IReturnValue<User> GetAllUsers()
        {
            return DoTask<User>
                (
                    new DoBusinessTaskDelegateMethod<User>(
                        (r) =>
                        {
                            using (BookLibDBContainer container = new BookLibDBContainer())
                            {
                                var users = (from u in container.Users
                                              select new { u.Id, u.Name, u.Email, u.Role, u.RoleId });

                                return HandleEntitiesList<User>(ref r, users, false);
                            }
                        }
                ), nameof(GetAllUsers));
        }

        public IReturnValue<User> GetUserByEmail(string email)
        {
            return DoTask<User>
                (
                    new DoBusinessTaskDelegateMethod<User>(
                        (r) =>
                        {
                            using (BookLibDBContainer container = new BookLibDBContainer())
                            {
                                var users = (from u in container.Users
                                              where u.Email.ToLower().Trim() == email.ToLower().Trim()
                                              select new { u.Id, u.Name, u.Email, u.Role, u.RoleId }).SingleOrDefault();

                                return HandelSingleEntity<User>(ref r, users);
                            }
                        }
                ), nameof(GetUserByEmail));
        }

        public IReturnValue<User> GetUserById(int id)
        {
            return DoTask<User>
                (
                    new DoBusinessTaskDelegateMethod<User>(
                        (r) =>
                        {
                            using (BookLibDBContainer container = new BookLibDBContainer())
                            {
                                var users = (from u in container.Users
                                              where u.Id == id
                                              select new { u.Id, u.Name, u.Email, u.Role, u.RoleId }).SingleOrDefault();

                                return HandelSingleEntity<User>(ref r, users);
                            }
                        }
                ), nameof(GetUserById));
        }

        public IReturnValue<User> GetUserByName(string name)
        {
            return DoTask<User>
               (
                   new DoBusinessTaskDelegateMethod<User>(
                       (r) =>
                       {
                           using (BookLibDBContainer container = new BookLibDBContainer())
                           {
                               var users = (from u in container.Users
                                             where u.Name.ToLower().Trim() == name.ToLower().Trim()
                                             select new { u.Id, u.Name, u.Email, u.Role, u.RoleId }).SingleOrDefault();

                               return HandelSingleEntity<User>(ref r, users);
                           }
                       }
               ), nameof(GetUserByName));
        }

        public IReturnValue<User> UpdateUserByEmail(string email, string name, int roleId)
        {
            return DoTask<User>
               (
                   new DoBusinessTaskDelegateMethod<User>(
                       (r) =>
                       {
                           using (BookLibDBContainer container = new BookLibDBContainer())
                           {
                               User user = container.Users.Where(u => u.Email.ToLower().Trim() == email.ToLower().Trim()).FirstOrDefault() as User;

                               if (null == user)
                                   return BookLibStatusCode.STATUS_ERROR_NOT_FOUND;

                               user.Name = name;
                               user.RoleId = roleId;
                               int updatedCount = container.SaveChanges();

                               return BookLibStatusCode.STATUS_OK;
                           }
                       }
               ), nameof(UpdateUserByEmail));
        }
        #endregion

        #region Books
        public IReturnValue<Book> AddANewBook(string name, string description, int bookTypeId)
        {
            // TODO - AddANewBook
            throw new NotImplementedException();
        }

        public IReturnValue<Book> BorrowABook(string email, string bookName, DateTime startTime)
        {
            // TODO - BorrowABook
            throw new NotImplementedException();
        }

        public IReturnValue<Book> DeleteABook(string name)
        {
            // TODO - DeleteABook
            throw new NotImplementedException();
        }

        public IReturnValue<Book> GetAllBooks()
        {
            // TODO - GetAllBooks
            throw new NotImplementedException();
        }

        public IReturnValue<Book> GetBookById(int id)
        {
            // TODO - GetBookById
            throw new NotImplementedException();
        }

        public IReturnValue<Book> GetBookByName(string name)
        {
            // TODO - GetBookByName
            throw new NotImplementedException();
        }

        public IReturnValue<Book> GetBooksByBookType(int bookTypeId)
        {
            // TODO - GetBooksByBookType
            throw new NotImplementedException();
        }

        public IReturnValue<Book> GetBooksByStatus(int statusId)
        {
            // TODO - GetBooksByStatus
            throw new NotImplementedException();
        }

        public IReturnValue<Book> ReturnABook(string bookName, DateTime returnTime)
        {
            // TODO - ReturnABook
            throw new NotImplementedException();
        }

        public IReturnValue<Book> SearchBookByDescription(string keywords)
        {
            // TODO - SearchBookByDescription
            throw new NotImplementedException();
        }

        public IReturnValue<Book> SearchBookByName(string startKeywords)
        {
            // TODO - SearchBookByName
            throw new NotImplementedException();
        }

        public IReturnValue<Book> UpdateBookInfo(int bookId, string name, string description, int bookTypeId, int statusId)
        {
            // TODO - UpdateBookInfo
            throw new NotImplementedException();
        }
        #endregion

        #region Histories
        public IReturnValue<History> GetAllHistories()
        {
            // TODO - GetAllHistories
            throw new NotImplementedException();
        }

        public IReturnValue<History> GetHistoriesByBookName(string bookName)
        {
            // TODO - GetHistoriesByBookName
            throw new NotImplementedException();
        }

        public IReturnValue<History> GetHistoriesByBookName(string bookName, DateTime startTime, DateTime returnTime)
        {
            // TODO - GetHistoriesByBookName
            throw new NotImplementedException();
        }

        public IReturnValue<History> GetHistoriesByUserEmail(string email)
        {
            // TODO - GetHistoriesByUserEmail
            throw new NotImplementedException();
        }

        public IReturnValue<History> GetHistoriesByUserEmail(string email, DateTime startTime, DateTime returnTime)
        {
            // TODO - GetHistoriesByUserEmail
            throw new NotImplementedException();
        }

        public IReturnValue<History> UpdateHistory()
        {
            // TODO - UpdateHistory
            throw new NotImplementedException();
        }
        #endregion
    }
}
