using CanvasApp.ViewModels;
using CanvasApp.Views;
using Microsoft.Extensions.DependencyInjection; // Required for GetRequiredService

namespace CanvasApp;

public partial class MainPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public MainPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
    }

    private async void OnNavigateToInstructorViewClicked(object sender, EventArgs e)
    {
        var viewModel = _serviceProvider.GetRequiredService<InstructorViewModel>();
        await Navigation.PushAsync(new InstructorView(viewModel));
    }

    private async void OnNavigateToStudentViewClicked(object sender, EventArgs e)
    {
        var viewModel = _serviceProvider.GetRequiredService<StudentViewModel>();
        await Navigation.PushAsync(new StudentView(viewModel));
    }

    private async void CloseButton_Click(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}


