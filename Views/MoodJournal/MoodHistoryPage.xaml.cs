using LifeHub.ViewModels.MoodJournal;
namespace LifeHub.Views.MoodJournal;


public partial class MoodHistoryPage : ContentPage
{
    public MoodHistoryPage(MoodHistoryViewModel viewModel)
    {
        InitializeComponent();
        
        
        BindingContext = viewModel;
    }

}