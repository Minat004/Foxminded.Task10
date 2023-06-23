using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace University.WPF.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{

    public MainWindowViewModel()
    {
        Menu = App.AppHost!.Services.GetRequiredService<NavigationViewModel>();
    }

    public NavigationViewModel? Menu { get; }
}