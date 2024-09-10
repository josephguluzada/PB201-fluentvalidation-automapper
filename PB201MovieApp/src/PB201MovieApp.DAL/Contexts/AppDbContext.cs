﻿using Microsoft.EntityFrameworkCore;
using PB201MovieApp.Core.Entities;
using PB201MovieApp.DAL.Configurations;

namespace PB201MovieApp.DAL.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}


    public DbSet<Movie> Movies { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
