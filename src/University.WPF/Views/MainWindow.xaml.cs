using System;
using System.ComponentModel;
using System.Windows;
using University.Core.Interfaces;
using University.Core.Models;
using University.WPF.ViewModels;

namespace University.WPF.Views;

public partial class MainWindow
{
    public MainWindow(
        IWindowService windowService,
        ICourseService<Course> courseService,
        IGroupService<Group> groupService)
    {
        DataContext = new MainWindowViewModel(windowService, courseService, groupService);
        InitializeComponent();
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        Application.Current.Shutdown();
    }
}