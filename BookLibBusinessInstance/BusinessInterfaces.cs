using BookLib.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib.Business.Instance
{
    public interface BusinessInterfaces
    {
        Status[] GetAllStatus();
        BookType[] GetAllBookTypes();
        User GetUserByEmail(string email);
        Book GetBookById(int id);
        Book GetBookByName(string name);
    }
}
