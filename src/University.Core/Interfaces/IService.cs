namespace University.Core.Interfaces;

public interface IStudentService<T> : IReadable<T>, IChangeable<T> { }

public interface ITeacherService<T> : IReadable<T>, IChangeable<T> { }

public interface ICourseService<T> : ICourseReadable, IReadable<T> { }

public interface IGroupService<T> : IGroupReadable, IReadable<T>, IChangeable<T> { }