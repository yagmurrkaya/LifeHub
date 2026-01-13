using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using LifeHub.Models.MoodJournal;
using LifeHub.Services;

namespace LifeHub.ViewModels.MoodJournal;

public class MoodJournalViewModel : BindableObject
{
    private readonly IMoodService _moodService;
    
    private string _selectedMood = string.Empty;
    private string _noteText = string.Empty;
    private string _statusMessage = string.Empty;

    public ObservableCollection<MoodEntry> RecentEntries { get; } = new();

    public string SelectedMood { get => _selectedMood; set { _selectedMood = value; OnPropertyChanged(); OnPropertyChanged(nameof(MoodStatusText)); } }
    public string NoteText { get => _noteText; set { _noteText = value; OnPropertyChanged(); } }
    public string StatusMessage { get => _statusMessage; set { _statusMessage = value; OnPropertyChanged(); } }
    public string MoodStatusText => string.IsNullOrEmpty(SelectedMood) ? "Select your mood:" : $"You are {SelectedMood} today";

    public ICommand SelectMoodCommand { get; }
    public ICommand SaveEntryCommand { get; }
    public ICommand DeleteEntryCommand { get; }
    public ICommand ClearAllCommand { get; }

    public MoodJournalViewModel(IMoodService moodService)
    {
        _moodService = moodService;

        UpdateRecentEntries();
        _moodService.GetEntries().CollectionChanged += (s, e) => UpdateRecentEntries();

        SelectMoodCommand = new Command<string>((mood) => SelectedMood = mood);
        SaveEntryCommand = new Command(async () => await ExecuteSaveEntry());
        DeleteEntryCommand = new Command<MoodEntry>(async (entry) => await ExecuteDeleteEntry(entry));
        
        // --- GÜNCELLENDİ ---
        ClearAllCommand = new Command(async () => await ExecuteClearAll());
        // -------------------
    }

    private void UpdateRecentEntries()
    {
        var top5 = _moodService.GetEntries().Take(5).ToList();
        
        RecentEntries.Clear();
        foreach (var entry in top5)
        {
            RecentEntries.Add(entry);
        }
    }

    private async Task ExecuteSaveEntry()
    {
        if (string.IsNullOrEmpty(SelectedMood))
        {
            await Shell.Current.DisplayAlert("Eksik Bilgi", "Lütfen önce bir mood seçin.", "Tamam");
            return;
        }

        if (string.IsNullOrWhiteSpace(NoteText))
        {
            await Shell.Current.DisplayAlert("Boş Not", "Lütfen günlüğünüze bir şeyler yazın.", "Tamam");
            return;
        }

        var newEntry = new MoodEntry
        {
            Mood = SelectedMood,
            Note = NoteText,
            Date = DateTime.Now
        };

        _moodService.AddEntry(newEntry);

        await Shell.Current.DisplayAlert("Başarılı", "Yeni mood eklendi! ✨", "Tamam");

        StatusMessage = $"Kaydedildi: {SelectedMood}";
        NoteText = string.Empty;
        SelectedMood = string.Empty;

        await Task.Delay(3000);
        StatusMessage = string.Empty;
    }

    // --- GÜNCELLENEN TEMİZLEME METODU ---
    private async Task ExecuteClearAll()
    {
        if (RecentEntries.Count == 0)
        {
            await Shell.Current.DisplayAlert("Bilgi", "Temizlenecek herhangi bir kayıt bulunmuyor.", "Tamam");
            return;
        }

        // Kullanıcıya onay sorusu sor
        bool answer = await Shell.Current.DisplayAlert(
            "Tümünü Temizle", 
            "Bütün mood girişlerin silinecek emin misin?", 
            "Evet", 
            "Hayır");

        if (answer)
        {
            // Onay verirse sil
            _moodService.ClearAll();
            
            StatusMessage = "Tüm kayıtlar temizlendi";
            
            // Kısa bir bilgi pop-up'ı göster
            await Shell.Current.DisplayAlert("Bilgi", "Bütün mood girişleri silindi!", "Tamam");
            
            await Task.Delay(2000);
            StatusMessage = string.Empty;
        }
        // Hayır derse metod hiçbir şey yapmadan sonlanır
    }

    private async Task ExecuteDeleteEntry(MoodEntry entry)
    {
        if (entry == null) return;

        bool answer = await Shell.Current.DisplayAlert(
            "Kaydı Sil", 
            "Bu günlük girişini silmek istediğinize emin misiniz?", 
            "Evet", 
            "Hayır");

        if (answer)
        {
            _moodService.DeleteEntry(entry); 
            StatusMessage = "Kayıt silindi";
            
            await Shell.Current.DisplayAlert("Bilgi", "Kayıt başarıyla silindi.", "Tamam");

            await Task.Delay(2000);
            StatusMessage = string.Empty;
        }
    }
}