using University.Core.Interfaces;
using University.Core.Models;

namespace University.Core.Services;

public class GroupService : IGroupService<Group>
{
    private readonly IGroupRepository<Group> _groupRepository;

    public GroupService(IGroupRepository<Group> groupRepository)
    {
        _groupRepository = groupRepository;
    }
    
    public async Task<IEnumerable<Group>> GetAllAsync()
    {
        return await _groupRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Student>> GetGroupStudentsAsync(int groupId)
    {
        return await _groupRepository.GetGroupStudentsAsync(groupId);
    }

    public async Task<Teacher> GetTeacherOrDefaultAsync(int teacherId)
    {
        return await _groupRepository.GetTeacherOrDefaultAsync(teacherId);
    }

    public async Task UpdateAsync(Group item)
    {
        await _groupRepository.UpdateAsync(item);
    }

    public async Task AddAsync(Group group)
    {
        await _groupRepository.AddAsync(group);
    }

    public async Task DeleteAsync(Group group)
    {
        await _groupRepository.DeleteAsync(group);
    }
}