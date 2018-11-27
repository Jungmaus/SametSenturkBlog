using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SametSenturkBlog.Data.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(20).IsRequired();
            builder.Property(x => x.SeoName).HasMaxLength(25).IsRequired();
            builder.Property(x => x.LanguageType).IsRequired();
        }
    }
}
