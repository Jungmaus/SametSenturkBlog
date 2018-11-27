using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Entities
{
    public class Tag:Base
    {

        public string Name { get; set; }
        public string SeoName { get; set; }

        public int ArticleID { get; set; }

        [ForeignKey("ArticleID")]
        public virtual Article Article { get; set; }
    }
}
