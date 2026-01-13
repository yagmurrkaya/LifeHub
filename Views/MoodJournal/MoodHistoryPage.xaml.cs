using LifeHub.ViewModels.MoodJournal;
namespace LifeHub.Views.MoodJournal;


public partial class MoodHistoryPage : ContentPage
{
    public MoodHistoryPage(MoodHistoryViewModel viewModel)
    {
        InitializeComponent();
        
        // MauiProgram.cs'den gelen hazır viewModel'i BindingContext'e bağlıyoruz
        BindingContext = viewModel;
    }

}