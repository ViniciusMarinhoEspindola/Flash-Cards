using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Infraestructure.Persistence
{
    public class DBContext(DbContextOptions<DBContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<UserLanguage> UserLanguages => Set<UserLanguage>();
        public DbSet<Card> Cards => Set<Card>();
        public DbSet<CardExample> CardExamples => Set<CardExample>();
        public DbSet<CardProgress> CardProgresses => Set<CardProgress>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);
        }
    }
}
