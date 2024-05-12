using CanvasApp.ViewModels;
using CanvasApp.Views;


namespace CanvasApp;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        MainPage = new NavigationPage(new MainPage(serviceProvider));
    }
}

