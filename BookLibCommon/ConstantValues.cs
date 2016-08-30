using BookLib.Resource;
using System.Resources;

namespace BookLib.Common
{
    public static class ConstantStrings
    {
        public const string DATE_FORMAT = "yyyy-MM-dd";
    }

    public static class BookLibStatusCode
    {
        private static BookLibResourceManager instance = BookLibResourceManager.Instance;

        public static string GetStatusMessage(string key)
        {
            return instance.GetString(key);
        }

        public static string GetStatusMessage(int code)
        {
            switch (code)
            {
                case STATUS_OK:
                    return instance.GetString(nameof(STATUS_OK));
                case STATUS_ERROR_NOT_FOUND:
                    return instance.GetString(nameof(STATUS_ERROR_NOT_FOUND));
                case STATUS_ERROR_UNKNOWN:
                    return instance.GetString(nameof(STATUS_ERROR_UNKNOWN));
            }

            return null;
        }

        public const int STATUS_OK = 0;
        public const int STATUS_ERROR_NOT_FOUND = 1;
        public const int STATUS_ERROR_EMPTY_LIST = 2;
        public const int STATUS_ERROR_UNKNOWN = 99;
    }
}
