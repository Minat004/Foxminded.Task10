using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using University.WPF.ViewModels;

namespace University.WPF.Views;

public partial class MainWindow
{
    public MainWindow()
    {
        DataContext = App.AppHost!.Services.GetRequiredService<MainWindowViewModel>();
        InitializeComponent();
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        Application.Current.Shutdown();
    }
}