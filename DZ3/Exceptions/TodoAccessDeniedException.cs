using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ3.Exceptions
{
    public class TodoAccessDeniedException : Exception
    {
        public TodoAccessDeniedException(String message) : base("User is not the owner of the Todo item: " + message + ".")
        {
        }
    }
}
