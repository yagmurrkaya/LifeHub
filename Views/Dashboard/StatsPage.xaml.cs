using LifeHub.ViewModels.Dashboard;

namespace LifeHub.Views.Dashboard;

public partial class StatsPage : ContentPage
{
    public StatsPage(StatsViewModel viewModel)
    {
        InitializeComponent();
        
        
        BindingContext = viewModel;
    }
}