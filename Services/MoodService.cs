using System.Collections.ObjectModel;
using LifeHub.Models.MoodJournal;

namespace LifeHub.Services;

public class MoodService : IMoodService
{
    private readonly ObservableCollection<MoodEntry> _entries = new();

    public MoodService()
    {
        // Uygulama açıldığında görünecek varsayılan veriler
        _entries = new ObservableCollection<MoodEntry>
        {
            new MoodEntry 
            { 
                Mood = "Happy", 
                Note = "LifeHub projesi harika ilerliyor, her şey çok şık oldu!", 
                Date = DateTime.Now 
            },
            new MoodEntry 
            { 
                Mood = "Tired", 
                Note = "Bugün çok fazla kod yazdık, biraz dinlenme vakti.", 
                Date = DateTime.Now.AddHours(-3) 
            },
            new MoodEntry 
            { 
                Mood = "Neutral", 
                Note = "Sıradan bir gündü, yarın için planlarımı yaptım.", 
                Date = DateTime.Now.AddDays(-1).AddHours(2) // Dün
            },
            new MoodEntry 
            { 
                Mood = "Sad", 
                Note = "Hava biraz kapalı, bugün kendimi pek enerjik hissetmiyorum. ", 
                Date = DateTime.Now.AddDays(-2) // 2 gün önce
            }
        };
    }

    public ObservableCollection<MoodEntry> GetEntries() => _entries;

    public void AddEntry(MoodEntry entry)
    {
        // En yeni kaydı her zaman başa ekle
        _entries.Insert(0, entry);

    }

    public void DeleteEntry(MoodEntry entry) => _entries.Remove(entry);

    public void ClearAll() => _entries.Clear();
}