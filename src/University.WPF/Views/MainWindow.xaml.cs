using System;
using System.ComponentModel;
using System.Windows;
using University.Core.Interfaces;
using University.Core.Models;
using University.WPF.ViewModels;

namespace University.WPF.Views;

public partial class MainWindow
{
    private readonly IDialogService _dialogService;

    public MainWindow(
        ICourseService<Course> courseService,
        IGroupService<Group> groupService,
        IStudentService<Student> studentService,
        ITeacherService<Teacher> teacherService,
        IDialogService dialogService)
    {
        _dialogService = dialogService;
        DataContext = new MainWindowViewModel(courseService, groupService, studentService, teacherService, dialogService);
        InitializeComponent();
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        Application.Current.Shutdown();
    }
}