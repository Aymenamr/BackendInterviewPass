namespace InterviewPass.WebApi.Controllers
{
    public interface IJobValidator
    {
        bool JobExists(string title);
    }
}
