using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SametSenturkBlog.Data.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Mapping
{
    public class ContactMap : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(x => x.IpAdress).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(40).IsRequired();
            builder.Property(x => x.Message).HasMaxLength(350).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(20).IsRequired();
            builder.Property(x => x.SubjectID).IsRequired();
            builder.Property(x => x.UserID).IsRequired();
            builder.Property(x => x.LanguageType).IsRequired();
        }
    }
}
