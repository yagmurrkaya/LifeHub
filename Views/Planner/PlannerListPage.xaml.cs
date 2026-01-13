namespace LifeHub.Views.Planner;
using LifeHub.ViewModels.Planner;
using LifeHub.Models.Planner;    // TaskItem'ı tanısın diye


public partial class PlannerListPage : ContentPage
{
    public PlannerListPage(PlannerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    // KRİTİK: Ayarlardan bu sayfaya her geri dönüldüğünde bu metod çalışır!
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is PlannerViewModel viewModel)
        {
            // Sayfa açılır açılmaz tamamlananları temizle kuralını işletiyoruz
            viewModel.PurgeCompletedTasks();

            // 2. Sıralama ayarını uygula (Yeni ekledik)
            viewModel.ApplySorting();
        }
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PlannerSettingsPage());
    }

    // PlannerListPage.xaml.cs içine bu metodu ekle (OnTaskToggled yerine)
    private void OnRadioButtonChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton rb && rb.BindingContext is TaskItem task)
        {
            // RadioButton'ın yeni durumunu modele aktar
            task.IsDone = e.Value;

            if (BindingContext is PlannerViewModel viewModel)
            {
                // Tamamlandıysa ve ayar açıksa silme kontrolünü yap
                viewModel.CheckAndAutoDelete(task);
            }
        }
    }
}