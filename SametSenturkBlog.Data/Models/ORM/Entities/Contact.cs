using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Entities
{
    public class Contact:Base
    {
        public string IpAdress { get; set; }

        public int SubjectID { get; set; }

        public string Message { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int UserID { get; set; }

        //[ForeignKey("UserID")]
        //public virtual User User { get; set; }
    }
}
