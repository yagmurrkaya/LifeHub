using LifeHub.ViewModels.HabitTracker;

namespace LifeHub.Views.HabitTracker;

public partial class HabitEditPage : ContentPage
{
    public HabitEditPage(HabitEditViewModel viewModel)
    {
        InitializeComponent();
        
       
        BindingContext = viewModel;
    }
}