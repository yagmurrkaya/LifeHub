// Views/Dashboard/DashboardPage.xaml.cs

using LifeHub.ViewModels.Dashboard;

namespace LifeHub.Views.Dashboard;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(DashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Sayfa her açıldığında sayıları ve son görevleri güncelle
        if (BindingContext is DashboardViewModel vm)
        {
            vm.RefreshData();
        }
    }
}