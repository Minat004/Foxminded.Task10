using University.Core.Models;

namespace University.WPF.ViewModels;

public class GroupViewModel : UnitedEntityViewModel
{
    public GroupViewModel(Group group) : base(group.Id, group.Name)
    {
    }
}