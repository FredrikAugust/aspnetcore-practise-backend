namespace Forest.Data.Models;

public class Challenge
{
    public int ChallengeId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Points { get; set; }
}