﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace GeneralHelper
{
    public static class ListHelper
    {
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dt = new DataTable();
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) 
                                ? Nullable.GetUnderlyingType(prop.PropertyType) 
                                : prop.PropertyType);
                dt.Columns.Add(prop.Name, type);

            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }
    }
}
