
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVCrud_super;

public partial class DbSuperContext : DbContext
{
    public DbSuperContext()
    {
    }

    public DbSuperContext(DbContextOptions<DbSuperContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
