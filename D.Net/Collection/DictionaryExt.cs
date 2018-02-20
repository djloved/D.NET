using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace D.Net.Collection
{
    public static class DictionaryExt
    {
        public static Dictionary<string, object> ToDictionary(this object data)
        {
            var attr = BindingFlags.Public | BindingFlags.Instance;
            var dict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            foreach (var property in data.GetType().GetProperties(attr))
            {
                if (property.CanRead)
                {
                    dict.Add(property.Name, property.GetValue(data, null));
                }
            }
            return dict;
        }
    }
}
