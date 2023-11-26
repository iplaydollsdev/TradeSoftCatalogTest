using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using TradeSoftCatalogTest.MVVM.Model;

public class AnalogContext : DbContext
{
    public DbSet<AnalogModel> AnalogModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        string connectionString = ConfigurationManager.ConnectionStrings["AnalogContext"].ConnectionString;
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AnalogModel>()
            .ToTable("analogs")
            .HasKey(a => a.Id);
    }
}