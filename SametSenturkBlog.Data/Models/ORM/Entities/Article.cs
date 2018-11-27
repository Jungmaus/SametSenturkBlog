using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Entities
{
    public class Article:Base
    {
        public Article()
        {
            this.Images = new List<Image>();
            this.Tags = new List<Tag>();
        }

        public string Subject { get; set; }
        public string SeoSubject { get; set; }

        public string Content { get; set; }

        public int ReadCount { get; set; }
        public int LikeCount { get; set; }
        public int DontLikeCount { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        public virtual List<Image> Images { get; set; }
        public virtual List<Tag> Tags { get; set; }
    }
}
