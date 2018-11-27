using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SametSenturkBlog.Data.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Mapping
{
    public class ArticleMap:IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(x => x.CategoryID).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Subject).HasMaxLength(50).IsRequired();
            builder.Property(x => x.SeoSubject).HasMaxLength(55).IsRequired();
            builder.Property(x => x.UserID).IsRequired();
            builder.Property(x => x.LanguageType).IsRequired();
        }
    }
}
