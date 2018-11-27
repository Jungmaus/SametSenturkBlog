using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SametSenturkBlog.Data.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SametSenturkBlog.Data.Models.ORM.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Username).HasMaxLength(15).IsRequired();
            builder.Property(x => x.Surname).HasMaxLength(15).IsRequired();
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(15).IsRequired();
            builder.Property(x => x.IpAdress).HasMaxLength(20).IsRequired(false);
            builder.Property(x => x.Email).HasMaxLength(40).IsRequired();
        }
    }
}
