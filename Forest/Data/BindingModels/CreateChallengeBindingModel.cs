namespace Forest.Data.BindingModels;

public class CreateChallengeBindingModel
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public int Points { get; set; }

    public string Answer { get; set; }
}