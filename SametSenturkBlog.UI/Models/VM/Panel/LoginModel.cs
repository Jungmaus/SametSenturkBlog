using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SametSenturkBlog.UI.Models.VM.Panel
{
    public class LoginModel
    {
        [MaxLength(15),Required]
        public string Username { get; set; }
        [MaxLength(15), Required]
        public string Password { get; set; }
    }
}
