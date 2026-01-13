using System.Collections.ObjectModel;
using LifeHub.Models.Planner;

namespace LifeHub.Services;

public interface IPlannerService
{
    ObservableCollection<TaskItem> GetTasks();
    void AddTask(TaskItem task);
    void RemoveTask(TaskItem task);
}

public class PlannerService : IPlannerService
{
    private readonly ObservableCollection<TaskItem> _tasks = new();

    public PlannerService()
    {
        _tasks.Add(new TaskItem { Description = "Ödevleri kontrol et 📝", Timestamp = "10:00", IsDone = false });
    }

    public ObservableCollection<TaskItem> GetTasks() => _tasks;

    public void AddTask(TaskItem task) => _tasks.Insert(0, task);

  public void RemoveTask(TaskItem task)
    {
        if (task == null) return;

        // Nesne zaten ObservableCollection içinde olduğu için direkt referansla silebiliriz
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (_tasks.Contains(task))
            {
                _tasks.Remove(task);
            }
        });
    }
}