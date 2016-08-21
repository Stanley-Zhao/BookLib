using BookLib.Common;
using System.Text;

namespace BookLib.DataModel
{
    public partial class History
    {
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("ID\t{0}\r\n", Id.ToString());
            builder.AppendFormat("Start\t{0}\r\n", StartTime.ToString(ConstantStrings.DATE_FORMAT));
            builder.AppendFormat("End\t{0}\r\n", ReturnTime.ToString(ConstantStrings.DATE_FORMAT));
            builder.AppendFormat("Book\t{0}\r\n", Book.Name);
            builder.AppendFormat("Type\t{0}\r\n", Book.BookType.Name);
            builder.AppendFormat("By\t{0}/{1}/{2}:\r\n", User.Role?.Name, User.Name, User.Email);
            builder.AppendLine();

            return builder.ToString();
        }
    }
}
