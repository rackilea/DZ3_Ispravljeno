using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace zad2.Models
{
    public class AddTodoViewModel
    {
        [Required(ErrorMessage = "Text is required.")]
        public string Text { get; set; }
    }
}
