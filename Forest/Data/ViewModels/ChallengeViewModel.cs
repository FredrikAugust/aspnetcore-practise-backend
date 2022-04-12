using Forest.Data.Models;

namespace Forest.Data.ViewModels;

public class ChallengeViewModel
{
    public Challenge Challenge { get; set; } = null!;

    public Answer? Answer { get; set; } = null!;
    
    public bool Solved { get; set; }
}