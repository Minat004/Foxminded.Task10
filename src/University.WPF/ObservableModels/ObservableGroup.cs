using CommunityToolkit.Mvvm.ComponentModel;
using University.Core.Models;

namespace University.WPF.ObservableModels;

public class ObservableGroup : ObservableSelectedEntity
{
    private readonly Group _group;

    public ObservableGroup(Group group)
    {
        _group = group;
    }
    
    public int Id
    {
        get => _group.Id;
        set => SetProperty(_group.Id, value, _group, (group, id) => group.Id = id);
    }
    
    public string Name
    {
        get => _group.Name;
        set => SetProperty(_group.Name, value, _group, (group, name) => group.Name = name);
    }
}