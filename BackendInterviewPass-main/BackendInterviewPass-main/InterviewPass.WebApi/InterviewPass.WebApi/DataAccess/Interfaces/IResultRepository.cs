namespace InterviewPass.WebApi.DataAccess.Interfaces
{
    public interface IResultRepository
    {
        Task<IEnumerable<Result>> GetAllAsync();
        Task<Result> GetByIdAsync(int id);
        Task AddAsync(Result result);
        void Update(Result result);
        void Delete(Result result);
    }
}
