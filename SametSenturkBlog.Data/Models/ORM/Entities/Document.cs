using System;
using System.Collections.Generic;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Entities
{
    public class Document:Base
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int DownloadCount { get; set; }
    }
}
