using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SametSenturkBlog.Data.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Mapping
{
    public class ImageMap : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Path).IsRequired();
            builder.Property(x => x.ArticleID).IsRequired(false);
            builder.Property(x => x.LanguageType).IsRequired(false);
        }
    }
}
