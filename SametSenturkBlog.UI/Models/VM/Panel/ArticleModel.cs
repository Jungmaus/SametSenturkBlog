using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SametSenturkBlog.UI.Models.VM.Panel
{
    public class ArticleModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ImgPath { get; set; }
        public string Language { get; set; }
        public int DescriptionCount { get; set; }
        public int SeeCount { get; set; }
        public int LikeCount { get; set; }
        public int DontLikeCount { get; set; }
        public string CategoryName { get; set; }
    }
}
