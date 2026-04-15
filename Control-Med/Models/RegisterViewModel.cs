using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Control_Med.Models
{
    public class RegisterViewModel
    {
            [Required]
            public  string Email { get; set; }
            public string Password { get; set; }
            public string Rol { get; set; }
        
    }
}