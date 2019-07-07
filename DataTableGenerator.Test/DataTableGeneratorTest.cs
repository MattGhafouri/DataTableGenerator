
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Linq.Expressions; 
using DataTableGenerator.Sample.Helper;
using DataTableGenerator.Sample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTableGenerator.Test
{
    [TestClass]
    public class DataTableGeneratorTest
    {
        [TestMethod]
        public void ToDataTable_NullCheckTest()
        {
            IEnumerable<Product> data = null;

            var datatable = data.ToDataTable<Product>();
             
            Assert.IsNotNull(datatable);
            Assert.IsTrue(datatable.Rows.Count == 0);
        }

        [TestMethod]
        public void ToDataTable_EmptyListTest()
        {
            IEnumerable<Product> data = new List<Product>();

            var datatable = data.ToDataTable<Product>();

            Assert.IsNotNull(datatable);
            Assert.IsTrue(datatable.Rows.Count == 0);
        }
         
        [TestMethod]
        public void ToDataTable_WithResourceManager()
        {
            var data = DataProvider.GenerateProducts();
            var resourceManager = DataProvider.GetLocalResourceManager();
            var productObject = new Product();
            var idPropertyTranslation = "Product Id";
            var namePropertyTranslation = "Product Name";
            var addressPropertyTranslation = "Company Address";


            var datatable = data.ToDataTable<Product>(resourceManager);
            
            Assert.IsNotNull(datatable);
            Assert.IsTrue(datatable.Rows.Count > 0);
            Assert.IsTrue(datatable.Columns[0].Caption == idPropertyTranslation);
            Assert.IsTrue(datatable.Columns[1].Caption == namePropertyTranslation);
            Assert.IsTrue(datatable.Columns[2].Caption == addressPropertyTranslation);
        }

        [TestMethod]
        public void ToDataTable_WithAllMembers()
        {
            var data = DataProvider.GenerateProducts();
            var properties = new Product().GetType().GetProperties();

            var datatable = data.ToDataTable<Product>(properties);

            Assert.IsNotNull(datatable);
            Assert.IsTrue(datatable.Rows.Count > 0);
            Assert.IsTrue(datatable.Columns.Count == properties.Count());
        }

        [TestMethod]
        public void ToDataTable_WithSpecificMembers()
        {
            var data = DataProvider.GenerateProducts();
            var properties = new Expression<Func<Product, string>>[2]
            {
                product => product.Name,
                product => product.Address
            };

            var datatable = data.ToDataTable(properties);

            Assert.IsNotNull(datatable);
            Assert.IsTrue(datatable.Rows.Count > 0);
            Assert.IsTrue(datatable.Columns.Count == properties.Count());
        }
    }
}
