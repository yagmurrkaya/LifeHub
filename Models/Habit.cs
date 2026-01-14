namespace LifeHub.Models;

public class Habit : BindableObject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Metric { get; set; } = string.Empty;
    public int Goal { get; set; }
    public int Streak { get; set; } = 0;

    private int _progress;
    public int Progress
    {
        get => _progress;
        set 
        { 
            _progress = value; 
            
            OnPropertyChanged(nameof(Progress)); 
            OnPropertyChanged(nameof(StatusDisplay)); 
        }
    }

    public string StatusDisplay => $"{Progress} / {Goal} {Metric}";
}