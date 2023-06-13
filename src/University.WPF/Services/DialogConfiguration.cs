using University.Core.Interfaces;

namespace University.WPF.Services;

public class DialogConfiguration : IDialogConfiguration
{
    public string? Title { get; set; }
    
    public int Height { get; set; }
    
    public int Width { get; set; }
}