namespace InterviewPass.WebApi.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IResultRepository ResultRepository { get; }
        Task<int> SaveAsync();
    }
}
