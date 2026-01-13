using LifeHub.ViewModels.Dashboard;

namespace LifeHub.Views.Dashboard;

public partial class StatsPage : ContentPage
{
    public StatsPage(StatsViewModel viewModel)
    {
        InitializeComponent();
        
        // KRİTİK: Eğer bu satır yoksa sayfa boş görünür!
        BindingContext = viewModel;
    }
}