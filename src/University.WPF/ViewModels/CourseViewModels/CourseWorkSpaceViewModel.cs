using Microsoft.Extensions.Configuration;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.CourseViewModels;

public partial class CourseWorkSpaceViewModel : UnitedEntityViewModel
{
    public CourseWorkSpaceViewModel(
        ICourseService<Course> courseService,
        IGroupService<Group> groupService,
        IStudentService<Student> studentService,
        ITeacherService<Teacher> teacherService,
        IDialogService dialogService,
        ICsvService csvService,
        IPdfService pdfService,
        IConfiguration configuration,
        int id, string name)
        : base(id, name)
    {
        CourseSideMenu = 
            new CourseSideTreeViewModel(
                courseService, groupService, studentService, teacherService, dialogService,
                csvService, pdfService, configuration);
    }
    
    public CourseSideTreeViewModel CourseSideMenu { get; }
}