using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SametSenturkBlog.UI.Models.VM.Home
{
    public class ArticleListVM
    {
        public string Title { get; set; }
        public string AddDate { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public string CategorySeoLink { get; set; }
        public string ArticleSeoLink { get; set; }
    }
}
