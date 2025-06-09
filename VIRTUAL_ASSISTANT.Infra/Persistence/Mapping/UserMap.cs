using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Domain.Entities.Users;

namespace VIRTUAL_ASSISTANT.Infra.Persistence.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TB_USER");

            builder.HasKey(x => x.UserId);

            builder.Property(x => x.UserId)
                    .HasColumnName("USER_ID")
                    .ValueGeneratedOnAdd()
                    .UseMySQLAutoIncrementColumn("INT");

            builder.Property(x => x.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(100);

            builder.Property(x => x.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(100);

            builder.Property(x => x.SaltKey)
                    .IsRequired()
                    .HasColumnName("SALT_KEY")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(100);

            builder.Property(x => x.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(100);

            builder.Property(x => x.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("PHONE_NUMBER")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(100);

            builder.Property(x => x.Address)
                    .IsRequired()
                    .HasColumnName("ADDRESS")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(100);
        }
    }
}
