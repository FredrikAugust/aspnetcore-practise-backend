using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Solve
{
    public int SolveId { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime Timestamp { get; set; }

    public string Subject { get; set; } = null!;

    public Challenge Challenge { get; } = null!;
    public int ChallengeId { get; set; }
}