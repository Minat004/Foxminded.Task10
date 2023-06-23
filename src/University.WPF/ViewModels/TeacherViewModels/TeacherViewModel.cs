using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;
using University.WPF.Services;
using University.WPF.Views.UserControls.TeacherViews;

namespace University.WPF.ViewModels.TeacherViewModels;

public partial class TeacherViewModel : UnitedEntityViewModel
{
    private readonly ITeacherService<Teacher> _teacherService;
    private readonly IDialogService _dialogService;

    public TeacherViewModel(
        ITeacherService<Teacher> teacherService,
        IDialogService dialogService,
        Teacher teacher) : base(teacher.Id, teacher.FirstName)
    {
        _teacherService = teacherService;
        _dialogService = dialogService;

        Teacher = teacher;

        Teachers = new NotifyTask<ObservableCollection<Teacher>>(GetTeachersAsync());
    }

    [ObservableProperty] 
    private Teacher teacher;

    [ObservableProperty] 
    private Teacher? selectedItem;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<Teacher>> teachers;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(OpenTeacherEditDialogCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteStudentCommand))]
    private Teacher? selectedTeacher;
    
    [RelayCommand]
    private void OpenTeacherAddDialog()
    {
        IDialogConfiguration dialogConfiguration = new DialogConfiguration()
        {
            Title = "Add Teacher",
            Height = 250,
            Width = 400
        };
        
        var _ = (Teacher) _dialogService.ShowDialog(
            new TeacherAddDialogView(), 
            new TeacherAddDialogViewModel(_teacherService),
            dialogConfiguration, null!)!;

        Teachers = new NotifyTask<ObservableCollection<Teacher>>(GetTeachersAsync());
    }
    
    
    [RelayCommand(CanExecute = nameof(CanOpenTeacherEditDialogOrDeleteTeacher))]
    private void OpenTeacherEditDialog(Teacher oldTeacher)
    {
        IDialogConfiguration dialogConfiguration = new DialogConfiguration()
        {
            Title = "Edit Teacher",
            Height = 250,
            Width = 400
        };
        
        var _ = (Teacher) _dialogService.ShowDialog(
            new TeacherEditDialogView(), 
            new TeacherEditDialogViewModel(_teacherService),
            dialogConfiguration, oldTeacher)!;
        
        Teachers = new NotifyTask<ObservableCollection<Teacher>>(GetTeachersAsync());
    }

    [RelayCommand(CanExecute = nameof(CanOpenTeacherEditDialogOrDeleteTeacher))]
    private void DeleteStudent(Teacher oldTeacher)
    {
        _teacherService.DeleteAsync(oldTeacher);
        
        Teachers = new NotifyTask<ObservableCollection<Teacher>>(GetTeachersAsync());
    }
    
    private bool CanOpenTeacherEditDialogOrDeleteTeacher(Teacher? teacher)
    {
        return teacher is not null;
    }
    
    private async Task<ObservableCollection<Teacher>> GetTeachersAsync()
    {
        var teachers = await _teacherService.GetAllAsync().ConfigureAwait(false);
        
        var observeTeachers = new ObservableCollection<Teacher>(teachers);

        return observeTeachers;
    }
}