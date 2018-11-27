using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SametSenturkBlog.Data.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Mapping
{
    public class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.Property(x => x.Description).HasMaxLength(35);
            builder.Property(x => x.Subject).HasMaxLength(15);
            builder.Property(x => x.IpAdress).HasMaxLength(20);
            builder.Property(x => x.Type).IsRequired(false);
            builder.Property(x => x.LanguageType).IsRequired(false);
        }
    }
}
