
using System.Collections.ObjectModel;
using LifeHub.Models;
using LifeHub.Models.MoodJournal;
using LifeHub.Services;
using LifeHub.Views.Dashboard;

namespace LifeHub.ViewModels.Dashboard;

public class StatsViewModel : BindableObject
{
    private readonly IHabitService _habitService;
    private readonly IPlannerService _plannerService;
    private readonly IMoodService _moodService;

    // Habit İstatistikleri
    public double HabitCompletionRate => CalculateHabitRate();
    public ObservableCollection<Habit> CompletedHabits { get; } = new();
    public ObservableCollection<Habit> NotCompletedHabits { get; } = new();

    // Planner İstatistikleri
    public double PlannerCompletionRate => CalculatePlannerRate();

    // Mood İstatistikleri (Yüzde hesaplamaları)
    public double HappyPercent => CalculateMoodPercent("Happy");
    public double NeutralPercent => CalculateMoodPercent("Neutral");
    public double AngryPercent => CalculateMoodPercent("Angry");
    public double TiredPercent => CalculateMoodPercent("Tired");
    public double SadPercent => CalculateMoodPercent("Sad");

    public StatsViewModel(IHabitService habitService, IPlannerService plannerService, IMoodService moodService)
    {
        _habitService = habitService;
        _plannerService = plannerService;
        _moodService = moodService;
        LoadData();
    }

    private void LoadData()
    {
        var allHabits = _habitService.GetAll();
        CompletedHabits.Clear();
        NotCompletedHabits.Clear();

        foreach (var h in allHabits)
        {
            if (h.Progress >= h.Goal) CompletedHabits.Add(h);
            else NotCompletedHabits.Add(h);
        }
    }

    private double CalculateHabitRate()
    {
        var all = _habitService.GetAll();
        if (!all.Any()) return 0;
        return (double)all.Count(h => h.Progress >= h.Goal) / all.Count;
    }

    private double CalculatePlannerRate()
    {
        var tasks = _plannerService.GetTasks();
        if (!tasks.Any()) return 0;
        return (double)tasks.Count(t => t.IsDone) / tasks.Count;
    }

    private double CalculateMoodPercent(string moodType)
    {
        var entries = _moodService.GetEntries();
        if (!entries.Any()) return 0;
        return (double)entries.Count(e => e.Mood == moodType) / entries.Count;
    }
}