namespace InterviewPass.WebApi.DataAccess.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InterviewPassContext _context;

        public IResultRepository ResultRepository { get; }

        public UnitOfWork(InterviewPassContext context)
        {
            _context = context;
            ResultRepository = new ResultRepository(_context);
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
