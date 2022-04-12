using Forest.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Forest.Data.Contexts;

public class ChallengesContext : DbContext
{
    public DbSet<Challenge> Challenges { get; set; } = null!;
    public DbSet<ChallengeAttachment> ChallengeAttachments { get; set; } = null!;
    public DbSet<Answer> Answers { get; set; } = null!;
    public DbSet<Solve> Solves { get; set; } = null!;
    
    private string DbPath { get; }

    public ChallengesContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);

        DbPath = Path.Join(path, "challenges.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
}