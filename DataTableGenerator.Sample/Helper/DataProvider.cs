using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using DataTableGenerator.Sample.Model;

namespace DataTableGenerator.Sample.Helper
{
    public static class DataProvider
    {
        public static IEnumerable<Product> GenerateProducts()
        {
            return new List<Product>
            {
                new Product(1, "Phone", "Street 01"),
                new Product(2, "Glass", "Street 02"),
                new Product(3, "Mouse", "Street 03")
            };
        }

        public static ResourceManager GetLocalResourceManager()
        {
            return new ResourceManager("DataTableGenerator.Sample.Resources.TranslateResource", Assembly.GetExecutingAssembly());
        } 
    }
}
