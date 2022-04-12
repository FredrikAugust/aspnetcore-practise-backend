namespace Forest.Data.Models;

public class ChallengeAttachment
{
    public int ChallengeAttachmentId { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public int ChallengeId { get; set; }
    public Challenge Challenge { get; set; } = null!;
}