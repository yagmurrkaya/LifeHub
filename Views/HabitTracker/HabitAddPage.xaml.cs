using LifeHub.ViewModels.HabitTracker;

namespace LifeHub.Views.HabitTracker;

public partial class HabitAddPage : ContentPage
{
    
    public HabitAddPage(HabitAddViewModel viewModel)
    {
        InitializeComponent();
        
        
        BindingContext = viewModel;
    }
}