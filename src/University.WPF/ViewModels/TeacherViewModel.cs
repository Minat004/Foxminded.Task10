using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels;

public partial class TeacherViewModel : UnitedEntityViewModel
{
    private readonly ITeacherService<Teacher> _teacherService;

    public TeacherViewModel(
        ITeacherService<Teacher> teacherService,
        Teacher teacher) : base(teacher.Id, teacher.FirstName)
    {
        _teacherService = teacherService;
        
        Teacher = teacher;

        Teachers = new NotifyTask<ObservableCollection<Teacher>>(GetTeachersAsync());
    }

    [ObservableProperty] 
    private Teacher teacher;

    [ObservableProperty] 
    private Teacher? selectedItem;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<Teacher>> teachers;
    
    private async Task<ObservableCollection<Teacher>> GetTeachersAsync()
    {
        var teachers = await _teacherService.GetAllAsync().ConfigureAwait(false);
        
        var observeTeachers = new ObservableCollection<Teacher>(teachers);

        return observeTeachers;
    }
}