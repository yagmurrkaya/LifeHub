using LifeHub.ViewModels.HabitTracker;

namespace LifeHub.Views.HabitTracker;

public partial class HabitAddPage : ContentPage
{
    // Dependency Injection ile ViewModel'i içeri alıyoruz
    public HabitAddPage(HabitAddViewModel viewModel)
    {
        InitializeComponent();
        
        // Bu satır sayfa ile mantık (ViewModel) arasındaki bağı kurar
        BindingContext = viewModel;
    }
}