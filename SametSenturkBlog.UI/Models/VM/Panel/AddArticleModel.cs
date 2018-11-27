using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SametSenturkBlog.UI.Models.VM.Panel
{
    public class AddArticleModel
    {
        public AddArticleModel()
        {
            LanguageTypes = new List<SelectListItem>();
        }

        public int ID { get; set; }

        public string PicPath { get; set; }
        public int LanguageType { get; set; }
        public int CategoryId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFile file { get; set; }

        //public Dictionary<string, string> Images { get; set; }

        public List<SelectListItem> CategoryList { get; set; }
        public List<SelectListItem> LanguageTypes { get; set; }
    }
}
