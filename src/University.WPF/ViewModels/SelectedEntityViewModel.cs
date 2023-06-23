using CommunityToolkit.Mvvm.ComponentModel;

namespace University.WPF.ViewModels;

public partial class SelectedEntityViewModel : ObservableObject
{
    [ObservableProperty] 
    private string? name;
}