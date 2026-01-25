using InterviewPass.DataAccess.Entities;
using InterviewPass.WebApi.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InterviewPass.WebApi.Authorization.Handlers
{
    public class ExamOwnerHandler : AuthorizationHandler<ExamOwnerRequirement, Exam>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExamOwnerRequirement requirement, Exam resource)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null && resource.CreatedBy ==  userId)
            {
                context.Succeed(requirement);
            }


            return Task.CompletedTask;
           
        }
    }
}
