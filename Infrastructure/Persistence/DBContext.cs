using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class DBContext(DbContextOptions<DBContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<Workspace> Workspaces => Set<Workspace>();
        public DbSet<Deck> Decks => Set<Deck>();
        public DbSet<Card> Cards => Set<Card>();
        public DbSet<CardExample> CardExamples => Set<CardExample>();
        public DbSet<CardProgress> CardProgresses => Set<CardProgress>();
        public DbSet<StudySession> StudySessions => Set<StudySession>();
        public DbSet<StudySessionAnswer> StudySessionAnswers => Set<StudySessionAnswer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);
        }
    }
}
