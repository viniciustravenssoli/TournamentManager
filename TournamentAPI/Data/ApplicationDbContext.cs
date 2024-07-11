using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TournamentAPI.Models;

namespace TournamentAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tournament>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tournaments)
            .HasForeignKey(t => t.UserId);
    }

    
}