using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources; 
using DataTableGenerator.Exception;

namespace DataTableGenerator
{
    internal static class Generator
    { 
        internal static void AddTableHeaderRow(DataTable table, MemberInfo[] members, ResourceManager resourceManager)
        {
            foreach (var memberInfo in members)
            {
                if (!(memberInfo is PropertyInfo)) continue;

                var propertyInfo = (PropertyInfo)memberInfo;
                var propertyType = propertyInfo.PropertyType;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    propertyType = Nullable.GetUnderlyingType(propertyType);
                var resourceKey = GetResourceKey(propertyInfo, resourceManager);

                if (string.IsNullOrEmpty(resourceKey))
                    throw new ResourceException($"{propertyInfo.Name} has not related translation key");

                table.Columns.Add(resourceKey, propertyType);
            }
        }

        internal static MemberInfo[] GetMemberInfo<T, TResult>(params Expression<Func<T, TResult>>[] expression)
        {
            var lst = new List<MemberInfo>();
            foreach (var item in expression)
            {
                var member = item.Body as MemberExpression;
                if (member != null)
                    lst.Add(member.Member);
            }
            return lst.ToArray();
        }

        internal static void AddTableBodyRows<ListType>(DataTable table, IEnumerable<ListType> list, MemberInfo[] members, ResourceManager resourceManager)
        {
            foreach (var record in list)
            {
                var dataRow = table.NewRow();
                foreach (var memberInfo in members)
                {
                    if (!(memberInfo is PropertyInfo)) continue;

                    var propertyInfo = (PropertyInfo)memberInfo;
                    var propertyValue = propertyInfo.GetValue(record, null);
                    var resourceKey = GetResourceKey(propertyInfo, resourceManager);

                    if (propertyValue == null)
                    {
                        if (resourceKey != null) dataRow[resourceKey] = DBNull.Value;
                    }
                    else if (resourceKey != null)
                    {
                        dataRow[resourceKey] = propertyValue.ToString();
                    }
                }
                table.Rows.Add(dataRow);
            }
        } 

        internal static MemberInfo[] GetListMembers<ListType>(IEnumerable<ListType> list)
        {
            if (list == null || !list.Any()) return null;

            var topRecord = list.ElementAtOrDefault(0);
            return ((Type)topRecord.GetType()).GetProperties();
        }

        internal static string GetResourceKey(PropertyInfo propertyInfo, ResourceManager resourceManager)
        {
            var nameAnnotation = string.Empty;
            var resourceKey = string.Empty;

            if (propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false).Length != 0)
            {
                nameAnnotation = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().Single().Name;
                resourceKey = TranslateHeader(resourceManager, nameAnnotation);
            }
            else
            {
                nameAnnotation = propertyInfo.Name;
                resourceKey = propertyInfo.Name;
            }
            return resourceKey;
        }

        internal static string TranslateHeader(ResourceManager resourceManager, string value)
        {
            if (resourceManager == null) return value;
            return resourceManager.GetString(value);
        }
         
    }
}
