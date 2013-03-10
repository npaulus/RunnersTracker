using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using RunnersTracker.DataAccess.Entities;

namespace RunnersTracker.DataAccess.Concrete
{
    public class RunnersTrackerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Shoes> Shoes { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
    }
}
