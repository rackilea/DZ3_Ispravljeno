using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2.Exceptions
{
    class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException(String message) : base(message)
        {
        }
    }
}
