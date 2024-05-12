using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using CanvasApp.Services;
using CanvasApp.Views;
using CanvasApp.ViewModels;
using Microsoft.Maui.Storage;

namespace CanvasApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
               .ConfigureFonts(fonts =>
               {
                   fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
               });

        // Register your services and ViewModels
        builder.Services.AddSingleton<InstructorViewModel>();
        builder.Services.AddSingleton<StudentViewModel>();
        builder.Services.AddSingleton<StudentDataService>();

        // Setup the database path and register DatabaseHelper
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "canvasapp.db");
        builder.Services.AddSingleton<DatabaseHelper>(new DatabaseHelper(dbPath));

        return builder.Build();
    }
}
