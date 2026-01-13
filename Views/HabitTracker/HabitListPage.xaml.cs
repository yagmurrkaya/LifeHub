using LifeHub.ViewModels.HabitTracker;

namespace LifeHub.Views.HabitTracker;

public partial class HabitListPage : ContentPage
{

    private readonly HabitListViewModel _viewModel;
    public HabitListPage(HabitListViewModel viewModel)
    {
        InitializeComponent();
        // Bu satır, XAML içindeki {Binding} ifadelerinin ViewModel'i görmesini sağlar.
        _viewModel = viewModel;
        BindingContext = _viewModel; 
    }

    // Sayfa her ekrana geldiğinde (yeni eklemeden dönünce dahil) çalışır
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadHabits(); // Listeyi refresh eder 
    }
}