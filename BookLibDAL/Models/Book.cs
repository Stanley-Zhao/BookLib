using BookLibDAL.Common;
using System.Text;

namespace BookLibDAL
{
    public partial class Book
    {
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("ID\t{0}\r\n", Id.ToString());
            builder.AppendFormat("Name\t{0}\r\n", Name);
            builder.AppendFormat("Des.\t{0}\r\n", Description);
            builder.AppendFormat("Status\t{0}\r\n", Status.Name);
            builder.AppendFormat("Type\t{0}\r\n", BookType.Name);
            builder.AppendFormat("Hist.\t({0}):\r\n", Histories.Count.ToString());
            builder.AppendLine("----");
            foreach (History history in Histories)
            {
                builder.AppendFormat("  Start - {0} | End - {1} | By {2}/{3}\r\n",
                    history.StartTime.ToString(ConstantStrings.DATE_FORMAT),
                    history.ReturnTime.ToString(ConstantStrings.DATE_FORMAT),
                    history.User.Name,
                    history.User.Email
                    );
            }
            builder.AppendLine("====");

            return builder.ToString();
        }
    }
}
