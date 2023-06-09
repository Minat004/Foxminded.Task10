namespace University.Core.Interfaces;

public interface IStudentRepository<T> : IReadable<T>, IChangeable<T> { }

public interface ITeacherRepository<T> : IReadable<T>, IChangeable<T> { }

public interface ICourseRepository<T> : ICourseReadable, IReadable<T> { }

public interface IGroupRepository<T> : IGroupReadable, IReadable<T>, IChangeable<T> { }