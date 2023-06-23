namespace University.Core.Models;

public partial class Group
{
    public int Id { get; set; }

    public int? CourseId { get; set; }

    public string Name { get; set; } = null!;

    public int? TeacherId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Teacher? Teacher { get; set; }
}
