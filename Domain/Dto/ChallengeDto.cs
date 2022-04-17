using Domain.Entities;

namespace Domain.Dto;

public class ChallengeDto
{
    public Challenge Challenge { get; set; } = null!;

    public Answer? Answer { get; set; } = null!;
    
    public bool Solved { get; set; }
}