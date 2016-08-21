using BookLib.Business.Instance;
using BookLib.DataAccessLayer;
using BookLib.DataModel;
using Business.Common;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BookLib.Business
{
    public class BookLibBL : BusinessInterfaces
    {
        #region Singleton
        class Nested
        {
            internal static readonly BookLibBL instance = new BookLibBL();

            static Nested()
            {
            }
        }

        private BookLibBL() { }

        public static BookLibBL Instance
        {
            get
            {
                return Nested.instance;
            }
        }
        #endregion

        public Status[] GetAllStatus()
        {
            using (BookLibDBContainer container = new BookLibDBContainer())
            {
                var status = (from s in container.Status
                              select new { s.Id, s.Name });
                List<Status> list = Utility.ConvertAnonymousTypeList<Status>(status) as List<Status>;
                return list.ToArray();
            }

        }

        public BookType[] GetAllBookTypes()
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Book GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        public Book GetBookByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
