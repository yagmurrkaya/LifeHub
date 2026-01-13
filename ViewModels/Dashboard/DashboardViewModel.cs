// ViewModels/Dashboard/DashboardViewModel.cs
using LifeHub.Services;
using System.Windows.Input;
using LifeHub.Models.MoodJournal; // MoodEntry'yi tanımak için ekledik
using LifeHub.Views.Dashboard; // StatsPage'e navigasyon için

namespace LifeHub.ViewModels.Dashboard;

public class DashboardViewModel : BindableObject
{
    private readonly IHabitService _habitService;
    private readonly IPlannerService _plannerService;
    private readonly IMoodService _moodService;

    // Özet Verileri
    public string CompletedHabitsCount => $"{_habitService.GetAll().Count(h => h.Progress >= h.Goal)} Alışkanlık Tamamlandı";
    public string LastTask => _plannerService.GetTasks().FirstOrDefault()?.Description ?? "Henüz görev yok";
    //public string LatestMood => _moodService.GetEntries().FirstOrDefault()?.Mood ?? "Henüz kayıt yok";
    // BURASI DEĞİŞTİ: Artık sadece mood ismini değil, tüm nesneyi döndürüyoruz
    public MoodEntry LatestMoodEntry => _moodService.GetEntries().FirstOrDefault();

    // Navigasyon Komutları
    public ICommand NavigateToHabitCommand { get; }
    public ICommand NavigateToPlannerCommand { get; }
    public ICommand NavigateToMoodCommand { get; }
    public ICommand NavigateToStatsCommand { get; }

    public DashboardViewModel(IHabitService habitService, IPlannerService plannerService, IMoodService moodService)
    {
        _habitService = habitService;
        _plannerService = plannerService;
        _moodService = moodService;

        // Sayfa geçişleri
        NavigateToHabitCommand = new Command(async () => await Shell.Current.GoToAsync("//HabitListPage"));
        NavigateToPlannerCommand = new Command(async () => await Shell.Current.GoToAsync("//PlannerListPage"));
        NavigateToMoodCommand = new Command(async () => await Shell.Current.GoToAsync("//MoodJournalPage"));
        NavigateToStatsCommand = new Command(async () => 
        {
            // nameof(StatsPage) kullanarak hata yapma riskini sıfıra indiriyoruz
            await Shell.Current.GoToAsync(nameof(StatsPage));
        });
    }

    // Verilerin güncellenmesi için bir metod (Sayfa her açıldığında çağrılacak)
    public void RefreshData()
    {
        OnPropertyChanged(nameof(CompletedHabitsCount));
        OnPropertyChanged(nameof(LastTask));
        OnPropertyChanged(nameof(LatestMoodEntry));
    }
}