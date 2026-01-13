using LifeHub.Models;
using System.Collections.Generic;

namespace LifeHub.Services
{
    public class HabitService : IHabitService
    {
        private readonly List<Habit> _habits = new();

        public HabitService()
        {
            // Uygulama ilk açıldığında eklenecek varsayılan Alışkanlıklar
            AddHabit(new Habit { Name = "Su İç", Metric = "Bardak", Goal = 8, Progress = 0 });
            AddHabit(new Habit { Name = "Yürüyüş Yap", Metric = "Adım", Goal = 10000, Progress = 0 });
            AddHabit(new Habit { Name = "Kitap Oku", Metric = "Dakika", Goal = 30, Progress = 0 });
        }

        public IList<Habit> GetAll()
        {
            return _habits;
        }

        public void AddHabit(Habit habit)
        {
            _habits.Add(habit);
        }

        public void RemoveHabit(Habit habit)
        {
            _habits.Remove(habit);
        }

        public void UpdateHabit(Habit updatedHabit)
        {
            var index = _habits.FindIndex(h => h.Id == updatedHabit.Id);
            if (index != -1)
            {
                _habits[index] = updatedHabit;
            }
        }
    }
}