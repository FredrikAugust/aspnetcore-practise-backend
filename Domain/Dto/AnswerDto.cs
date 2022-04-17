namespace Domain.Dto;

public class AnswerDto
{
    public enum Status
    {
        Correct,
        Incorrect
    }

    public Status AnswerStatus { get; set; }
}