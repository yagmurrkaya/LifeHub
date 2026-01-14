using LifeHub.ViewModels.HabitTracker;

namespace LifeHub.Views.HabitTracker;

public partial class HabitListPage : ContentPage
{

    private readonly HabitListViewModel _viewModel;
    public HabitListPage(HabitListViewModel viewModel)
    {
        InitializeComponent();
        
        _viewModel = viewModel;
        BindingContext = _viewModel; 
    }

    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadHabits(); // Listeyi refresh eder 
    }
}