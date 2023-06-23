using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using University.Core.Interfaces;
using University.Core.Models;
using University.WPF.ViewModels;

namespace University.WPF.ObservableModels;

public partial class ObservableHeader : ObservableSelectedEntity
{
    private readonly ICourseService<Course> _courseService;

    public ObservableHeader(int id, string name, ICourseService<Course> courseService)
    {
        _courseService = courseService;
        Id = id;
        Name = name;

        Courses = new NotifyTask<ObservableCollection<ObservableCourse>>(GetCoursesAsync());
    }

    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private string name;
    
    [ObservableProperty] 
    private NotifyTask<ObservableCollection<ObservableCourse>> courses;
    
    private async Task<ObservableCollection<ObservableCourse>> GetCoursesAsync()
    {
        var courses = await _courseService.GetAllAsync().ConfigureAwait(false);
        
        var observableCourses = courses.Select(course => 
            new ObservableCourse(course, _courseService));

        var observableCollection = new ObservableCollection<ObservableCourse>(observableCourses);

        return observableCollection;
    }
}