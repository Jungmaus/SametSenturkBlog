using System;
using System.Collections.Generic;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Entities
{
    public class Category:Base
    {
        public Category()
        {
            this.Articles = new List<Article>();
        }

        public string Name { get; set; }
        public string SeoName { get; set; }

        public virtual List<Article> Articles { get; set; }
    }
}
