using System;
using System.Collections.Generic;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Entities
{
    public class User:Base
    {
        public User()
        {
            this.Contacts = new List<Contact>();
            this.Articles = new List<Article>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? LastLogin { get; set; }
        public string IpAdress { get; set; }
        public int Role { get; set; }

        public virtual List<Contact> Contacts { get; set; }
        public virtual List<Article> Articles { get; set; }
    }
}
