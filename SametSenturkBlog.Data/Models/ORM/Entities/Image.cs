using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Entities
{
    public class Image:Base
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public int? Type { get; set; }

        public int? ArticleID { get; set; }

        [ForeignKey("ArticleID")]
        public virtual Article Article { get; set; }
    }
}
