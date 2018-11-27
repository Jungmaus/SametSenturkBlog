using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SametSenturkBlog.Data.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Mapping
{
    public class DocumentMap : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.Property(x => x.Description).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Language).HasMaxLength(10).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(35).IsRequired();
            builder.Property(x => x.Path).IsRequired();
            builder.Property(x => x.LanguageType).IsRequired();
        }
    }
}
