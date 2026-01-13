using Microsoft.UI.Xaml;

namespace LifeHub.WinUI;

public partial class App : MauiWinUIApplication
{
    public App()
    {
        this.InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    // BU BLOK: Windows açıldığında MAUI servislerinin doğru yüklenmesini sağlar
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        // Uygulamanın Windows üzerinde ana pencereyi (Shell) oluşturması için gerekli tetikleyici
        Microsoft.Maui.MauiWinUIApplication.Current.Services.GetRequiredService<Microsoft.Maui.IApplication>();
    }
}