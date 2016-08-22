using System.Resources;

namespace BookLib.Common
{
    public static class ConstantStrings
    {
        public const string DATE_FORMAT = "yyyy-MM-dd";
    }

    public static class StatusCode
    {
        //public static string GetStatusMessage(int code)
        //{
        //    //ResourceManager rm = 

        //    switch(code)
        //    {
        //        case OK:
        //            return rm.GetString("Status_OK");
        //        case UNKNOWN:
        //            return rm.GetString("Status_OK_Error_Unknown");
        //    }

        //    return null;
        //}

        public const int OK = 0;
        public const int UNKNOWN = 99;
    }
}
