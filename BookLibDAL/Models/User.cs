using BookLibDAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibDAL
{
    public partial class User
    {
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("ID\t{0}\r\n", Id.ToString());
            builder.AppendFormat("Name\t{0}\r\n", Name);
            builder.AppendFormat("Email\t{0}\r\n", Email);
            builder.AppendFormat("Role\t{0}\r\n", Role?.Name);
            builder.AppendFormat("Hist.\t({0}):\r\n", Histories?.Count.ToString());
            foreach (History history in Histories)
            {
                builder.AppendFormat("  Start - {0} | End - {1} | By {2}/{3}/{4}\r\n",
                    history.StartTime.ToString(ConstantStrings.DATE_FORMAT),
                    history.ReturnTime.ToString(ConstantStrings.DATE_FORMAT),
                    history.User.Role.Name,
                    history.User.Name,
                    history.User.Email
                    );
            }

            builder.AppendLine();

            return builder.ToString();
        }
    }
}
