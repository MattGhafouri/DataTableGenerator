using System.ComponentModel.DataAnnotations;
using DataTableGenerator.Sample.Resources;

namespace DataTableGenerator.Sample.Model
{
    public class Product
    {
        public Product()
        {
            
        }
        public Product(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        [Display(Name = "Id", ResourceType = typeof(TranslateResource))]
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(TranslateResource))]
        public string Name { get; set; }

        [Display(Name = "Address", ResourceType = typeof(TranslateResource))]
        public string Address { get; set; }
    }
}
