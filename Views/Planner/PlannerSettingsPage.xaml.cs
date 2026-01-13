namespace LifeHub.Views.Planner;
using LifeHub.ViewModels.Planner;

public partial class PlannerSettingsPage : ContentPage
{
	public PlannerSettingsPage()
	{
		InitializeComponent();
		BindingContext = new PlannerSettingsViewModel();
	}
}