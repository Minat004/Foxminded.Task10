using University.Core.Models;

namespace University.Core.Interfaces;

public interface IPdfService
{
    void SaveReport(Group group);
}