using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LifeHub.Models.Planner; 

public class TaskItem : INotifyPropertyChanged
{
    private bool _isDone;
    public string Description { get; set; } = string.Empty; 
    public string Timestamp { get; set; } = string.Empty;    

    public bool IsDone
    {
        get => _isDone;
        set { _isDone = value; OnPropertyChanged(); OnPropertyChanged(nameof(BackgroundColor)); }
    }

    public Color BackgroundColor => IsDone ? Color.FromArgb("#F3F4F6") : Colors.White;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}