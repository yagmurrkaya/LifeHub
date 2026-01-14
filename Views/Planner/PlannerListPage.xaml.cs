namespace LifeHub.Views.Planner;
using LifeHub.ViewModels.Planner;
using LifeHub.Models.Planner;    


public partial class PlannerListPage : ContentPage
{
    public PlannerListPage(PlannerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is PlannerViewModel viewModel)
        {
            
            viewModel.PurgeCompletedTasks();

           
            viewModel.ApplySorting();
        }
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PlannerSettingsPage());
    }

    private void OnRadioButtonChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton rb && rb.BindingContext is TaskItem task)
        {
            task.IsDone = e.Value;

            if (BindingContext is PlannerViewModel viewModel)
            {
                viewModel.CheckAndAutoDelete(task);
            }
        }
    }
}