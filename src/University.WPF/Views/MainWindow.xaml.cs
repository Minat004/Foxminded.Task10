using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Extensions.Configuration;
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
        IDialogService dialogService,
        ICsvService csvService,
        IConfiguration configuration)
    {
        _dialogService = dialogService;
        DataContext = new MainWindowViewModel(courseService, groupService, studentService, 
            teacherService, dialogService, configuration, csvService);
        InitializeComponent();
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        Application.Current.Shutdown();
    }
}