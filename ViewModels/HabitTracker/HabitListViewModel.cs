using System.Collections.ObjectModel;
using System.Windows.Input;
using LifeHub.Models;
using LifeHub.Services;
using LifeHub.Views.HabitTracker;

namespace LifeHub.ViewModels.HabitTracker;

public class HabitListViewModel : BindableObject
{
    private readonly IHabitService _habitService;
    
    // Listeyi burada direkt oluşturuyoruz (Initialize)
    public ObservableCollection<Habit> Habits { get; } = new ObservableCollection<Habit>();

    private string _progressSummary = string.Empty;
    public string ProgressSummary { get => _progressSummary; set { _progressSummary = value; OnPropertyChanged(); } }

    public ICommand IncrementCommand { get; }
    public ICommand DecrementCommand { get; }
    public ICommand GoToAddPageCommand { get; }
    public ICommand GoToEditPageCommand { get; }

    private string _todayDate = string.Empty;
    public string TodayDate 
    { 
        get => _todayDate; 
        set { _todayDate = value; OnPropertyChanged(); } 
    }

    // public HabitListViewModel(IHabitService habitService)
    // {
    //     _habitService = habitService;

        

    //     Habits = new ObservableCollection<Habit>();

    //     // Komut tanımlamaları

    //     TodayDate = DateTime.Now.ToString("📅 dd MMMM dddd");
    //     IncrementCommand = new Command<Habit>(h => 
    //     { 
    //         if (h.Progress < h.Goal) 
    //         {
    //             h.Progress++; 
    //             UpdateSummary(); 
    //         }
    //     });
    //     DecrementCommand = new Command<Habit>(h => { if (h.Progress > 0) h.Progress--; UpdateSummary(); });

    //     GoToAddPageCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(HabitAddPage)));
    //     GoToEditPageCommand = new Command<Habit>(async (h) => 
    //         await Shell.Current.GoToAsync(nameof(HabitEditPage), new Dictionary<string, object> { { "SelectedHabit", h } }));

    //     // ÖNCE listeyi doldur, SONRA özeti güncelle
    //     LoadHabits();
    // }

    public HabitListViewModel(IHabitService habitService)
    {
        _habitService = habitService;

        // 1. Gereksiz ikinci "Habits = new..." satırını sildik.
        TodayDate = DateTime.Now.ToString("📅 dd MMMM dddd");

        // 2. Komutlara null kontrolü ekledik
        IncrementCommand = new Command<Habit>(h => 
        { 
            if (h != null && h.Progress < h.Goal) 
            {
                h.Progress++; 
                UpdateSummary(); 
            }
        });

        DecrementCommand = new Command<Habit>(h => 
        { 
            if (h != null && h.Progress > 0) 
            {
                h.Progress--; 
                UpdateSummary(); 
            }
        });

        GoToAddPageCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(HabitAddPage)));
        
        GoToEditPageCommand = new Command<Habit>(async (h) => 
            await Shell.Current.GoToAsync(nameof(HabitEditPage), new Dictionary<string, object> { { "SelectedHabit", h } }));

        LoadHabits();
    }

    public void UpdateSummary()
    {
        if (Habits == null) return;
        int completed = Habits.Count(h => h.Progress >= h.Goal);
        ProgressSummary = $"Tamamlanan: {completed} / {Habits.Count} 🎯";
    }

    public void LoadHabits()
    {
        var updatedList = _habitService.GetAll();
        
        // Habits artık null değil, güvenle Clear yapabiliriz
        Habits.Clear();
        foreach (var habit in updatedList)
        {
            Habits.Add(habit);
        }
        
        UpdateSummary();
    }
}