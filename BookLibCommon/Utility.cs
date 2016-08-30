using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Common
{
    public class Utility
    {
        // Converts a list of objects into a list of strongly typed objects.        
        public static IEnumerable<T> ConvertAnonymousTypeList<T>(IEnumerable<object> list)
        {
            if (list == null)
                return null;

            if (list.Count() == 0)
                return new List<T>();

            var stronglyTypedList = (List<T>)Activator.CreateInstance(typeof(List<T>), null);

            foreach (var item in list)
            {
                T obj = ConvertAnonymousType<T>(item);
                stronglyTypedList.Add(obj);
            }

            return stronglyTypedList;
        }

        public static T ConvertAnonymousType<T>(object obj)
        {
            if (obj == null)
                return default(T);

            var stronglyTypedObj = (T)Activator.CreateInstance(typeof(T), null);
            foreach (System.Reflection.PropertyInfo pi in typeof(T).GetProperties())
            {
                var value = obj.GetType().GetProperty(pi.Name)?.GetValue(obj);
                typeof(T).GetProperty(pi.Name).SetValue(stronglyTypedObj, value, null);
            }
            return stronglyTypedObj;
        }

    }
}
