namespace Domain.Entities;

public class Answer
{
    public int AnswerId { get; set; }

    public string Value { get; set; } = null!;

    public Challenge Challenge { get; set; } = null!;
    public int ChallengeId { get; set; }
}