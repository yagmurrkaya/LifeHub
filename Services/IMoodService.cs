using System.Collections.ObjectModel;
using LifeHub.Models.MoodJournal;

namespace LifeHub.Services;

public interface IMoodService
{
    ObservableCollection<MoodEntry> GetEntries();
    void AddEntry(MoodEntry entry);
    void DeleteEntry(MoodEntry entry);
    void ClearAll();
}