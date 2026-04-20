using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;

namespace InterviewPass.WebApi.Controllers
{
    public class JobValidator :IJobValidator
    {
        private readonly IGenericRepository<Job> _jobRepository;

        public JobValidator(IGenericRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public bool JobExists(string title)
        {
            return _jobRepository.GetByProperty(jb => jb.Title == title) != null;
        }
    }
}
