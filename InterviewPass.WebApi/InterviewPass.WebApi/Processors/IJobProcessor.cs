using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Processors
{
	public interface IJobProcessor
	{
		JobModel ProcessAddJob(JobModel job);

		JobModel ProcessUpdateJob(string id, JobModel job);

	}
}
