using System;
using Microsoft.Maui.Controls;
using CanvasApp.ViewModels;  // Make sure this is correct according to your ViewModel's namespace


namespace CanvasApp.Views;

public partial class StudentView : ContentPage
{
    public StudentView(StudentViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}
