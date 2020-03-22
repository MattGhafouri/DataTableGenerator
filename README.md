# DataTableGenerator
To generate a datatable based on generic input list with header translation capability and determination of specific members of the list

 
There is a sample project in the solution, which uses this library and it is totally easy to understand.
If you had any problem, let me know.




# How to Use

1- You got a Model as Product like this :

```
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
```


2- You call a method and it have returned a list of product like this 

 ```
 IEnumerable<Procudt> result = myMethod();
 
 ```
 
3- If you need to convert the result to .net datatable with specific headers, you just need to use .ToDataTable extension of IEnumerable :
```
   var dataTableOverload01 = products.ToDataTable();
```

4- If you need to specify some of the Prodcut's property as datatable columns, call this overload :

```
  var dataTableOverload03 = products.ToDataTable((Product p) => p.Name, p => p.Address);
```

5- If you need to translate the datatable column's header with .NET ResourceManager, call this overload :
```
    var dataTableOverload02 = products.ToDataTable(resourceManager);
```



# Notice
If you need to translate specific properties in the result datatable, you need to decorate the properties with `Display` attribute
```
 [Display(Name = "Id", ResourceType = typeof(TranslateResource))]
 public int Id { get; set; }
```


# Nuget package Url

https://www.nuget.org/packages/DataTableGenerator

