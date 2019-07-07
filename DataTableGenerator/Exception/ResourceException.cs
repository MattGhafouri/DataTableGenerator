using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTableGenerator.Exception
{
    public class ResourceException : System.Exception
    {
        public ResourceException(string message) 
            : base(message)
        {
            
        }
    }
}
