using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using University.Core.Interfaces;
using University.Core.Models;
using University.WPF.ViewModels;

namespace University.WPF.ObservableModels;

public partial class ObservableCourse : ObservableSelectedEntity
{
    private readonly Course _course;
    private readonly ICourseService<Course> _courseService;

    public ObservableCourse(Course course, ICourseService<Course> courseService)
    {
        _course = course;
        _courseService = courseService;

        GroupsByCourse = new NotifyTask<ObservableCollection<ObservableGroup>>(GetGroupsByCourseAsync());
    }

    public int Id
    {
        get => _course.Id;
        set => SetProperty(_course.Id, value, _course, (course, id) => course.Id = id);
    }
    
    public string Name
    {
        get => _course.Name;
        set => SetProperty(_course.Name, value, _course, (course, name) => course.Name = name);
    }
    
    [ObservableProperty] 
    private NotifyTask<ObservableCollection<ObservableGroup>> groupsByCourse;

    private async Task<ObservableCollection<ObservableGroup>> GetGroupsByCourseAsync()
    {
        var courseGroups = await _courseService.GetCourseGroupsAsync(Id).ConfigureAwait(false);
        
        var observableCourseGroups = courseGroups.Select(group => new ObservableGroup(group));

        var observableCollection = new ObservableCollection<ObservableGroup>(observableCourseGroups);

        return observableCollection;
    }
}