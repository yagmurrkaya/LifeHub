

// ViewModels/MoodJournal/MoodHistoryViewModel.cs
using System.Collections.ObjectModel;
using LifeHub.Models.MoodJournal;
using LifeHub.Services;
using Microsoft.Maui.Controls;
using System.Windows.Input;
using System.Linq;

namespace LifeHub.ViewModels.MoodJournal;

public class MoodHistoryViewModel : BindableObject
{
    private readonly IMoodService _moodService;
    public ObservableCollection<MoodGroup> GroupedEntries { get; set; } = new();

    //public ICommand BackCommand => new Command(async () => await Shell.Current.GoToAsync(".."));

    public MoodHistoryViewModel(IMoodService moodService)
    {
        _moodService = moodService;
        LoadGroupedEntries();
    }

    private void LoadGroupedEntries()
    {
        var allEntries = _moodService.GetEntries();

        if (allEntries == null || !allEntries.Any()) return;

        // Verileri tarihe göre gruplayıp yeni bir listeye alıyoruz
        var sortedGroups = allEntries
            .GroupBy(e => e.Date.Date)
            .OrderByDescending(g => g.Key)
            .Select(g => new MoodGroup(
                GetFriendlyDate(g.Key), 
                g.OrderByDescending(x => x.Date).ToList() // Saat sırasına da sokalım
            ))
            .ToList();

        GroupedEntries.Clear();
        foreach (var group in sortedGroups)
        {
            GroupedEntries.Add(group);
        }
    }

    private string GetFriendlyDate(DateTime date)
    {
        if (date.Date == DateTime.Today) return "Bugün - " + date.ToString("dd MMMM yyyy dddd");
        if (date.Date == DateTime.Today.AddDays(-1)) return "Dün - " + date.ToString("dd MMMM yyyy dddd");
        return date.ToString("dd MMMM yyyy dddd");
    }
}