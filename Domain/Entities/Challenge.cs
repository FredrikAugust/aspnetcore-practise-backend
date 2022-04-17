namespace Domain.Entities;

public class Challenge
{
    public int ChallengeId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Points { get; set; }

    public List<ChallengeAttachment> Attachments { get; } = new();

    public Answer Answer { get; set; } = null!;
}
