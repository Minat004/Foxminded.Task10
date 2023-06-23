using CommunityToolkit.Mvvm.ComponentModel;

namespace University.WPF.ObservableModels;

public abstract partial class ObservableSelectedEntity : ObservableObject
{
    [ObservableProperty]
    private bool isSelected;

    [ObservableProperty] 
    private bool isExpanded;
}