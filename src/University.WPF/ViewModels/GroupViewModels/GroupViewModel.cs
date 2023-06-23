using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using University.Core.Interfaces;
using University.Core.Models;
using University.Core.Models.Mapping;
using University.WPF.Services;
using University.WPF.ViewModels.StudentViewModels;
using University.WPF.Views.UserControls.StudentViews;

namespace University.WPF.ViewModels.GroupViewModels;

public partial class GroupViewModel : UnitedEntityViewModel
{
    private readonly IGroupService<Group> _groupService;
    private readonly IStudentService<Student> _studentService;
    private readonly IDialogService _dialogService;
    private readonly ICsvService _csvService;
    private readonly IPdfService _pdfService;
    private readonly IConfiguration _configuration;

    public GroupViewModel(
        IGroupService<Group> groupService,
        IStudentService<Student> studentService,
        IDialogService dialogService,
        ICsvService csvService,
        IPdfService pdfService,
        IConfiguration configuration,
        Group group) 
        : base(group.Id, group.Name)
    {
        _groupService = groupService;
        _studentService = studentService;
        _dialogService = dialogService;
        _csvService = csvService;
        _pdfService = pdfService;
        _configuration = configuration;

        Group = group;

        StudentsByGroup = new NotifyTask<ObservableCollection<Student>>(GetStudentsByGroupAsync());

        Students = new NotifyTask<ObservableCollection<Student>>(GetStudentsAsync());
    }

    [ObservableProperty] 
    private Group group;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<Student>> studentsByGroup;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<Student>> students;

    [ObservableProperty] 
    private GroupViewModel? selectedItem;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(OpenStudentEditDialogCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteStudentCommand))]
    private Student? selectedStudent;
    
    [RelayCommand]
    private void OpenStudentAddDialog()
    {
        IDialogConfiguration dialogConfiguration = new DialogConfiguration()
        {
            Title = "Add Student",
            Height = 250,
            Width = 400
        };
        
        var student = (Student) _dialogService.ShowDialog(
            new StudentAddWindowView(), 
            new StudentAddDialogViewModel(_groupService, _studentService),
            dialogConfiguration, null!)!;

        StudentsByGroup = new NotifyTask<ObservableCollection<Student>>(GetStudentsByGroupAsync());

        Students = new NotifyTask<ObservableCollection<Student>>(GetStudentsAsync());
    }
    
    
    [RelayCommand(CanExecute = nameof(CanOpenStudentEditDialogOrDeleteStudent))]
    private void OpenStudentEditDialog(Student oldStudent)
    {
        IDialogConfiguration dialogConfiguration = new DialogConfiguration()
        {
            Title = "Edit Student",
            Height = 250,
            Width = 400
        };
        
        var student = (Student) _dialogService.ShowDialog(
            new StudentEditDialogView(), 
            new StudentEditDialogViewModel(_studentService),
            dialogConfiguration, oldStudent)!;
        
        StudentsByGroup = new NotifyTask<ObservableCollection<Student>>(GetStudentsByGroupAsync());

        Students = new NotifyTask<ObservableCollection<Student>>(GetStudentsAsync());
    }

    [RelayCommand(CanExecute = nameof(CanOpenStudentEditDialogOrDeleteStudent))]
    private void DeleteStudent(Student oldStudent)
    {
        _studentService.DeleteAsync(oldStudent);
        
        StudentsByGroup = new NotifyTask<ObservableCollection<Student>>(GetStudentsByGroupAsync());

        Students = new NotifyTask<ObservableCollection<Student>>(GetStudentsAsync());
    }

    [RelayCommand]
    private void ImportStudents()
    {
        var saveFileDialog = new SaveFileDialog()
        {
            Filter = "CSV files (*.csv)|*.csv|TXT files (*.txt)|*.txt"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            _csvService.Save<Student, StudentMapCsvSave>(saveFileDialog.FileName, StudentsByGroup.Result);
        }
    }

    [RelayCommand]
    private void ExportStudents()
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "CSV files (*.csv)|*.csv"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            var loadStudents = _csvService.Load<Student, StudentMapCsvLoad>(openFileDialog.FileName);

            foreach (var student in loadStudents)
            {
                _studentService.AddAsync(student!);
            }

            StudentsByGroup = new NotifyTask<ObservableCollection<Student>>(GetStudentsByGroupAsync());

            Students = new NotifyTask<ObservableCollection<Student>>(GetStudentsAsync());
        }
    }
    
    [RelayCommand]
    private void SaveReport()
    {
        Group.Students = StudentsByGroup.Result;
        _pdfService.SaveReport(Group);
    }

    private bool CanOpenStudentEditDialogOrDeleteStudent(Student? student)
    {
        return student is not null;
    }
    
    private async Task<ObservableCollection<Student>> GetStudentsByGroupAsync()
    {
        var students = await _groupService.GetGroupStudentsAsync(Group.Id).ConfigureAwait(false);
        
        var observeStudents = new ObservableCollection<Student>(students);

        return observeStudents;
    }

    private async Task<ObservableCollection<Student>> GetStudentsAsync()
    {
        var students = await _studentService.GetAllAsync().ConfigureAwait(false);
        
        var observeStudents = new ObservableCollection<Student>(students);

        return observeStudents;
    }
}