using LifeHub.Models;

namespace LifeHub.Services;

public interface IHabitService
{
    IList<Habit> GetAll();
    void AddHabit(Habit habit);
    void RemoveHabit(Habit habit);
    void UpdateHabit(Habit updatedHabit);
}
