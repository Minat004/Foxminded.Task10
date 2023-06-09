namespace University.Core.Interfaces;

public interface IChangeable<in T>
{
    public Task UpdateAsync(T item);
    
    public Task AddAsync(T item);
    
    public Task DeleteAsync(T item);
}