using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using DataTableGenerator.Exception;
using DataTableGenerator.Sample.Helper;
using DataTableGenerator.Sample.Model;

namespace DataTableGenerator.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var products = DataProvider.GenerateProducts();
                var resourceManager = DataProvider.GetLocalResourceManager();

                var dataTableOverload01 = products.ToDataTable();
                var dataTableOverload02 = products.ToDataTable(resourceManager);

                var dataTableOverload03 = products.ToDataTable((Product p) => p.Name, p => p.Address);
                var dataTableOverload04 = products.ToDataTable(resourceManager, (Product p) => p.Name, p => p.Address);

                var dataTableOverload05 = products.ToDataTable(GetMembers());
                var dataTableOverload06 = products.ToDataTable(GetMembers(), resourceManager);
            }
            catch (ResourceException e)
            {
                Console.WriteLine(e);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
            }
        }


        private static MemberInfo[] GetMembers()
        {
            return new Product().GetType().GetMembers();
        }
    }
}
