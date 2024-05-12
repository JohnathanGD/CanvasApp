using CanvasApp.ViewModels;
using CanvasApp.Views;

namespace CanvasApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(InstructorView), typeof(InstructorView));
        Routing.RegisterRoute(nameof(StudentView), typeof(StudentView));
    }
}

