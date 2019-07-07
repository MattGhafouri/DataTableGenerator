using System;
using System.Collections.Generic; 
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources; 


namespace DataTableGenerator
{
    /// <summary>
    /// To generate a datatable based on generic input list with header translation capability and determination of specific members of the list
    /// </summary>
    
    public static class DataTableGenerator
    {

        /// <typeparam name="ListType">The input list data type</typeparam>
        /// <param name="list">A generic list to create data table</param>
        /// (The result datatable members, need to be decorated with 'Display' attrbiute.
        /// For example   [Display(Name = "Id", ResourceType = typeof(Resource))] )</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable<ListType>(this IEnumerable<ListType> list)  
        { 
            return ToDataTable(list, Generator.GetListMembers(list), null);
        }


        /// <typeparam name="ListType">The input list data type</typeparam>
        /// <param name="list">A generic list to create data table</param>
        /// <param name="members">Exported datatable's columns which specifid with this members list</param>
        /// (The result datatable members, need to be decorated with 'Display' attrbiute.
        /// For example   [Display(Name = "Id", ResourceType = typeof(Resource))] )</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable<ListType>
            (this IEnumerable<ListType> list, MemberInfo[] members)  
        {
            return ToDataTable(list, members, null);
        }


        /// <typeparam name="ListType">The input list data type</typeparam>
        /// <param name="list">A generic list to create data table</param>
        /// <param name="resourceManager">To translate the result datatable's header
        /// (The result datatable members, need to be decorated with 'Display' attrbiute.
        /// For example   [Display(Name = "Id", ResourceType = typeof(Resource))] )</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable<ListType>
            (this IEnumerable<ListType> list, ResourceManager resourceManager)  
        {
            return ToDataTable(list, Generator.GetListMembers(list), resourceManager);
        }

        /// <typeparam name="ListType">The input list data type</typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="list">A generic list to create data table</param>
        /// <param name="expression">Expression to determine exporting members of the input list</param>
        /// (The result datatable members, need to be decorated with 'Display' attrbiute.
        /// For example   [Display(Name = "Id", ResourceType = typeof(Resource))] )</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable<ListType, TInput, TResult>
            (this IEnumerable<ListType> list, params Expression<Func<TInput, TResult>>[] expression) 
        {
            return ToDataTable(list, Generator.GetMemberInfo(expression), null);
        }



        /// <typeparam name="ListType">The input list data type</typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="list">A generic list to create data table</param>
        /// <param name="resourceManager">To Translate the result datatable's header ( To translate, the properties need to decorated with 'Display' attrbiute. For example   [Display(Name = "Id", ResourceType = typeof(TranslateResource))] )</param>
        /// <param name="expression">Expression to determine exporting members of the input list</param>
        /// <returns>DataTable</returns>
        /// (The result datatable members, need to be decorated with 'Display' attrbiute.
        /// For example   [Display(Name = "Id", ResourceType = typeof(Resource))] )</param>
        public static DataTable ToDataTable<ListType, TInput, TResult>
            (this IEnumerable<ListType> list, ResourceManager resourceManager, params Expression<Func<TInput, TResult>>[] expression) 
        {
            return ToDataTable(list, Generator.GetMemberInfo(expression), resourceManager);
        }


        /// <typeparam name="ListType">The input list data type</typeparam>
        /// <param name="list">A generic list to create data table</param>
        /// <param name="members">Exported datatable's columns which specifid with this members list</param>
        /// (The result datatable members, need to be decorated with 'Display' attrbiute.
        /// For example   [Display(Name = "Id", ResourceType = typeof(Resource))] )</param>
        /// <param name="resourceManager">To Translate the result datatable's header ( To translate, the properties need to decorated with 'Display' attrbiute. For example   [Display(Name = "Id", ResourceType = typeof(TranslateResource))] )</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable<ListType>
            (this IEnumerable<ListType> list, MemberInfo[] members, ResourceManager resourceManager) 
        {
            if (list == null || !list.Any() || members == null || !members.Any())
                return new DataTable();

            var dataTable = new DataTable();
            Generator.AddTableHeaderRow(dataTable, members, resourceManager);
            Generator.AddTableBodyRows(dataTable , list, members, resourceManager);

            return dataTable;
        }


    }
}