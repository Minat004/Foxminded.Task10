using CommunityToolkit.Mvvm.ComponentModel;

namespace University.WPF.ViewModels;

public partial class UnitedEntityViewModel : ObservableObject
{
    public UnitedEntityViewModel(int id, string name)
    {
        Id = id;
        Name = name;
    }

    [ObservableProperty] 
    private int id;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private bool isSelected;
}