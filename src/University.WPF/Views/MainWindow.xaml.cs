using System.Windows;
using University.Core.Interfaces;
using University.Core.Models;
using University.WPF.ViewModels;

namespace University.WPF.Views
{
    public partial class MainWindow
    {
        public MainWindow(
            ICourseService<Course> courseService,
            IGroupService<Group> groupService)
        {
            DataContext = new MainWindowViewModel(courseService, groupService);
            InitializeComponent();
        }
    }
}