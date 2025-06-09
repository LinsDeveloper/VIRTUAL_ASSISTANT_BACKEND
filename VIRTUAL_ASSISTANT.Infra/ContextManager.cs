using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Domain.Entities.Integrations.Reminders;
using VIRTUAL_ASSISTANT.Domain.Entities.Users;
using VIRTUAL_ASSISTANT.Infra.Persistence.Mapping;

namespace VIRTUAL_ASSISTANT.Infra
{
    public class ContextManager : DbContext
    {
        public ContextManager(DbContextOptions<ContextManager> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserRemindersMap());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserReminders> UserReminders { get; set; }

    }
}
