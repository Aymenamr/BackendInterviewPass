namespace InterviewPass.WebApi.DataAccess.Interfaces
{
    public class ResultRepository : IResultRepository
    {
        private readonly InterviewPassContext _context;

        public ResultRepository(InterviewPassContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Result>> GetAllAsync() => await _context.Results.ToListAsync();

        public async Task<Result> GetByIdAsync(int id) => await _context.Results.FindAsync(id);

        public async Task AddAsync(Result result) => await _context.Results.AddAsync(result);

        public void Update(Result result) => _context.Results.Update(result);

        public void Delete(Result result) => _context.Results.Remove(result);
    }
}
