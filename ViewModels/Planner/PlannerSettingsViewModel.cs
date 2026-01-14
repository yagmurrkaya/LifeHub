using System.ComponentModel;
using System.Runtime.CompilerServices;
using LifeHub.Models.Planner; 
using LifeHub.Services;       

namespace LifeHub.ViewModels.Planner;

public class PlannerSettingsViewModel : INotifyPropertyChanged
{
    // Ayar 1: Tamamlananları Otomatik Sil
    public bool AutoDeleteCompleted
    {
        get => Preferences.Default.Get("AutoDeleteTasks", false);
        set
        {
            Preferences.Default.Set("AutoDeleteTasks", value);
            OnPropertyChanged();
        }
    }

    public List<string> SortOptions { get; } = new() { "Zamana Göre", "A-Z Alfabetik" };

    // Ayar 2: Sıralama Türü
    public string SortOrder
    {
        get => Preferences.Default.Get("SortOrder", "Zamana Göre");
        set
        {
            Preferences.Default.Set("SortOrder", value);
            OnPropertyChanged();
        }
    }

    

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}