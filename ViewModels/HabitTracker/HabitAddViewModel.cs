using System.Windows.Input;
using LifeHub.Models;
using LifeHub.Services;

namespace LifeHub.ViewModels.HabitTracker;

public class HabitAddViewModel : BindableObject
{
    private readonly IHabitService _habitService;

    public List<string> MetricsList => Metrics.Available;

    private string _name = string.Empty;
    public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }

    private string _selectedMetric = string.Empty;
    public string SelectedMetric { get => _selectedMetric; set { _selectedMetric = value; OnPropertyChanged(); } }

    private int _goal;
    public int Goal { get => _goal; set { _goal = value; OnPropertyChanged(); } }

    // KOMUT TANIMLARI
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; } 

    public HabitAddViewModel(IHabitService habitService)
    {
        _habitService = habitService;
        SaveCommand = new Command(async () => await SaveHabit());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    private async Task SaveHabit()
    {
        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(SelectedMetric) || Goal <= 0)
        {
            await Shell.Current.DisplayAlert("Hata", "Lütfen tüm alanları doldurun.", "Tamam");
            return;
        }

        var newHabit = new Habit { Name = Name, Metric = SelectedMetric, Goal = Goal, Progress = 0 };
        _habitService.AddHabit(newHabit);
        await Shell.Current.DisplayAlert(
                    "Alışkanlık Eklendi!", 
                    $"{newHabit.Name} başarıyla eklendi.", 
                    "Tamam");
        await Shell.Current.GoToAsync("..");
    }
}