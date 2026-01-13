namespace LifeHub;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // MainPage yerine artık Window nesnesi döndürüyoruz
        return new Window(new AppShell());
    }
}