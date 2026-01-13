// Views/MoodJournal/MoodJournalPage.xaml.cs
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
        // AppShell'de "MoodHistoryPage" olarak kayıtlı olmalı
        await Shell.Current.GoToAsync(nameof(MoodHistoryPage));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Burada servislerden veri çekerken bir null referans hatası alıyor olabilir misin?
        // try-catch bloğuna alarak test edebilirsin.
    }
}