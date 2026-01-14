
using LifeHub.ViewModels.MoodJournal;

namespace LifeHub.Views.MoodJournal;

public partial class MoodJournalPage : ContentPage
{
    public MoodJournalPage(MoodJournalViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void GoToHistory_Clicked(object sender, EventArgs e)
    {
       
        await Shell.Current.GoToAsync(nameof(MoodHistoryPage));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
    }
}