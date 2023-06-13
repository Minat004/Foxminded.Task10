using System.ComponentModel;
using System.Net;
using System.Windows;
using University.Core.Interfaces;
using University.Core.Models;
using University.WPF.ViewModels;

namespace University.WPF.Views;

public partial class CreateGroupWindow : IWindowService
{
    public CreateGroupWindow(
        IGroupService<Group> groupService,
        ICourseService<Course> courseService,
        ITeacherService<Teacher> teacherService)
    {
        DataContext = new CreateGroupViewModel(groupService, courseService, teacherService);
        InitializeComponent();
    }

    public void ShowWindow()
    {
        ShowDialog();
    }

    public void CloseWindow()
    {
        Close();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        Visibility = Visibility.Collapsed;
        e.Cancel = true;
        base.OnClosing(e);
    }
}