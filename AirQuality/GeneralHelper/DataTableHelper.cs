using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GeneralHelper
{
    public static class DataTableHelper
    {
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            try
            {
                var list = new List<T>(dt.Rows.Count);
                var properties = typeof(T).GetProperties();
                foreach (DataRow row in dt.Rows)
                {
                    var obj = new T();
                    foreach (var property in properties)
                    {
                        try
                        {
                            property.SetValue(obj, row[property.Name]);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
            
        }
    }
}
