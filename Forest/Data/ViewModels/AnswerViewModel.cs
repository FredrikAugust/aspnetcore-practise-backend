namespace Forest.Data.ViewModels;

public class AnswerViewModel
{
    public enum Status
    {
        Correct,
        Incorrect
    }

    public Status AnswerStatus { get; set; }
}