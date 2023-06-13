namespace University.Core.Interfaces;

public interface IDialogConfiguration
{
    public string? Title { get; set; }

    public int Height { get; set; }

    public int Width { get; set; }
}