using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SametSenturkBlog.UI.Models.VM.Panel
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public int ArticleCount { get; set; }
        public DateTime AddDate { get; set; }

        public bool isDeletable { get; set; }
    }
}
