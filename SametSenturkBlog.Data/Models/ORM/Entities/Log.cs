using System;
using System.Collections.Generic;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Entities
{
    public class Log:Base
    {
        public int? Type { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string IpAdress { get; set; }
    }
}
