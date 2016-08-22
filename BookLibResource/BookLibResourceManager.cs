using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BookLib.Resource
{
    public class BookLibResourceManager
    {
        #region Singleton
        private BookLibResourceManager() { }

        public static BookLibResourceManager Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            internal static readonly BookLibResourceManager instance = new BookLibResourceManager();

            static Nested()
            {
            }
        }
        #endregion

        public string GetString(string key)
        {
            return BookLibResource.ResourceManager.GetString(key, BookLibResource.Culture);
        }

        public void SetCultureInfo(CultureInfo culture)
        {
            BookLibResource.Culture = culture;
        }
    }
}
