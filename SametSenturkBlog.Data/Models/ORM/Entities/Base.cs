using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Entities
{
    public class Base
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public bool isActive { get; set; }
        public bool isDeleted { get; set; }

        public DateTime AddDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public int? LanguageType { get; set; }
    }
}
