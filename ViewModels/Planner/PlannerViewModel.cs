using System.Collections.ObjectModel; 
using System.Windows.Input;
using LifeHub.Models.Planner; 
using LifeHub.Services;       
using System.Linq;

namespace LifeHub.ViewModels.Planner;

public class PlannerViewModel : BindableObject
{
    private readonly IPlannerService _plannerService;
    
    // Servis üzerinden merkezi listeye erişiyoruz
    public ObservableCollection<TaskItem> Tasks => _plannerService.GetTasks();

    private string _newTaskText = string.Empty;
    public string NewTaskText { get => _newTaskText; set { _newTaskText = value; OnPropertyChanged(); } }

    public string TodayDate => DateTime.Now.ToString("dddd, dd MMMM yyyy");
    public string DisplayDate => DateTime.Now.ToString("dddd, dd MMMM yyyy");

    // KOMUTLAR
    public ICommand AddTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }
    public ICommand ClearAllCommand { get; }

    public PlannerViewModel(IPlannerService plannerService)
    {
        _plannerService = plannerService;

        // Komut atamaları
        AddTaskCommand = new Command(ExecuteAddTask);
        
        
        DeleteTaskCommand = new Command<TaskItem>(async (task) => await ExecuteDeleteTask(task));

        
        ClearAllCommand = new Command(async () => await ExecuteClearAll());
    }

    // Görev Ekleme Fonksiyonu
    private async void ExecuteAddTask()
    {
        if (string.IsNullOrWhiteSpace(NewTaskText))
        {
            await Shell.Current.DisplayAlert("Uyarı", "Lütfen önce bir görev içeriği girin!", "Tamam");
            return;
        }

        _plannerService.AddTask(new TaskItem 
        { 
            Description = NewTaskText, 
            Timestamp = DateTime.Now.ToString("HH:mm:ss"), 
            IsDone = false 
        });

        NewTaskText = string.Empty;
        ApplySorting();
    }

    // Tümünü Temizleme Fonksiyonu (Onaylı)
    private async Task ExecuteClearAll()
    {
        if (Tasks.Count == 0)
        {
            await Shell.Current.DisplayAlert("Bilgi", "Temizlenecek herhangi bir planınız bulunmuyor.", "Tamam");
            return;
        }

        bool answer = await Shell.Current.DisplayAlert(
            "Tümünü Temizle", 
            "Bütün planlarınızı silmek istediğinize emin misiniz?", 
            "Evet", 
            "Hayır");

        if (answer)
        {
            Tasks.Clear();
            await Shell.Current.DisplayAlert("Bilgi", "Bütün planlar temizlendi!", "Tamam");
        }
    }

    // 2. ADIM: Yeni Sadeleştirilmiş Silme Fonksiyonu
    private async Task ExecuteDeleteTask(TaskItem task)
    {
        if (task == null) return;

        // ClearAll ile aynı uyarı formatı
        bool answer = await Shell.Current.DisplayAlert(
            "Sil", 
            "Bu planı silmek istediğinize emin misiniz?", 
            "Evet", 
            "Hayır");

        if (answer)
        {


            string taskName = task.Description;
            Tasks.Remove(task); 
            await Shell.Current.DisplayAlert("Bilgi", $"{taskName} silindi!", "Tamam");
        }
    }

    
    public void CheckAndAutoDelete(TaskItem task)
    {
        if (task == null) return;
        
        bool isAutoDelete = Preferences.Default.Get("AutoDeleteTasks", false);

        if (isAutoDelete && task.IsDone)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (Tasks.Contains(task)) Tasks.Remove(task);
            });
        }
    }

    public void PurgeCompletedTasks()
    {
        bool isAutoDelete = Preferences.Default.Get("AutoDeleteTasks", false);
        
        if (isAutoDelete)
        {
            // Tamamlanmış olanları bul ve listeden at
            var completedTasks = Tasks.Where(t => t.IsDone).ToList();
            foreach (var task in completedTasks)
            {
                Tasks.Remove(task);
            }
        }
    }

    public void ApplySorting()
    {
        // Tercihlerden hangi sıralamanın seçili olduğunu alıyoruz
        string sortOrder = Preferences.Default.Get("SortOrder", "Zamana Göre");

        if (Tasks == null || Tasks.Count <= 1) return;

        List<TaskItem> sorted;

        if (sortOrder == "A-Z Alfabetik")
        {
            // Description'a göre A'dan Z'ye sırala
            sorted = Tasks.OrderBy(t => t.Description).ToList();
        }
        else
        {
            // Zamana Göre (Timestamp) sırala
            sorted = Tasks.OrderByDescending(t => t.Timestamp).ToList();
        }

        // ObservableCollection'ı bozmadan elemanların yerini değiştiriyoruz
        for (int i = 0; i < sorted.Count; i++)
        {
            var item = sorted[i];
            int oldIndex = Tasks.IndexOf(item);
            if (oldIndex != i)
            {
                Tasks.Move(oldIndex, i); // Bu işlem UI'da güzel bir yer değiştirme animasyonu sağlar
            }
        }
    }




}