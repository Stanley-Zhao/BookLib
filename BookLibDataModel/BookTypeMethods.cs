﻿namespace BookLib.DataModel
{
    public partial class BookType
    {
        public override string ToString()
        {
            return string.Format("ID\t{0}\r\nName\t{1}\r\n", Id.ToString(), Name);
        }
    }
}
