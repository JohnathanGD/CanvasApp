using System;
using Microsoft.Maui.Controls;
using CanvasApp.ViewModels;  // Make sure this is correct according to your ViewModel's namespace
using CanvasApp.Services;



namespace CanvasApp.Views
{
    public partial class InstructorView : ContentPage
    {
        public InstructorView(InstructorViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
