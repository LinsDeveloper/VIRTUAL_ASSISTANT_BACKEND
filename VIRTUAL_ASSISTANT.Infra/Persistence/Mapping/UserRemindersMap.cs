using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.EntityFrameworkCore.Extensions;
using VIRTUAL_ASSISTANT.Domain.Entities.Integrations.Reminders;

namespace VIRTUAL_ASSISTANT.Infra.Persistence.Mapping
{
    public class UserRemindersMap : IEntityTypeConfiguration<UserReminders>
    {
        public void Configure(EntityTypeBuilder<UserReminders> builder)
        {
            builder.ToTable("TB_USER_REMINDERS");

            builder.HasKey(x => x.ReminderId);

            builder.Property(x => x.ReminderId)
                    .HasColumnName("REMINDER_ID")
                    .ValueGeneratedOnAdd()
                    .UseMySQLAutoIncrementColumn("INT");

            builder.Property<int>("UserId")
                    .HasColumnName("USER_ID");

            builder.Property(x => x.Title)
                    .IsRequired()
                    .HasColumnName("TITLE")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(100);

            builder.Property(x => x.ReminderTimer)
                    .IsRequired()
                    .HasColumnName("REMINDER_TIMER")
                    .HasColumnType("DATETIME");

            builder.Property(x => x.Message)
                    .IsRequired()
                    .HasColumnName("MESSAGE")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(100);

        }
    }
}
