using System.Windows.Input;
using LifeHub.Models;
using LifeHub.Services;

namespace LifeHub.ViewModels.HabitTracker;

[QueryProperty(nameof(CurrentHabit), "SelectedHabit")]
public class HabitEditViewModel : BindableObject
{
    private readonly IHabitService _habitService;
    public List<string> MetricsList => Metrics.Available;

    private Habit _currentHabit = null!;
    public Habit CurrentHabit { get => _currentHabit; set { _currentHabit = value; OnPropertyChanged(); } }

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }

    public HabitEditViewModel(IHabitService habitService)
    {
        _habitService = habitService;

        // Kaydetme Komutu
        SaveCommand = new Command(async () => 
        { 
            _habitService.UpdateHabit(CurrentHabit);
             
            await Shell.Current.DisplayAlert(
                "Alışkanlık Güncellendi!", 
                $"{CurrentHabit.Name} başarıyla güncellendi.", 
                "Tamam");
            await Shell.Current.GoToAsync(".."); 
        });

        // Silme Komutu (Onay Mekanizmalı)
        DeleteCommand = new Command(async () => 
        {
            // Kullanıcıya onay sorusu soruyoruz
            bool answer = await Shell.Current.DisplayAlert(
                "Silme Onayı", 
                "Alışkanlığı silmek istediğine emin misin?", 
                "Evet", 
                "Hayır");

            // Eğer kullanıcı "Evet" derse silme işlemini yap
            if (answer)
            {
                _habitService.RemoveHabit(CurrentHabit);
                await Shell.Current.DisplayAlert(
                    "Alışkanlık Silindi!", 
                    $"{CurrentHabit.Name} başarıyla silindi.", 
                    "Tamam");
                await Shell.Current.GoToAsync("..");
            }
            // "Hayır" derse hiçbir şey yapma, pop-up kendiliğinden kapanacaktır.
        });
    }
}