namespace University.Core.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
