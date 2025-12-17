using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Enums;
using InterviewPass.WebApi.Examples;
using InterviewPass.WebApi.Extensions;
using InterviewPass.WebApi.Models.User;
using InterviewPass.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Microsoft.AspNetCore.Http;
using InterviewPass.WebApi.Constants;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly Func<string, IUserRepository> _userRepoResolver;


        private readonly IMapper _mapper;
        public UserController(ILogger<UserController> logger, Func<string, IUserRepository> userRepoResolver, IMapper mapper)
        {
            _logger = logger;
            _userRepoResolver = userRepoResolver;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieve a User according to his Id
        /// </summary>
        /// <param name="login">the Login of the user</param>
        /// <returns></returns>
        /// <response code="200">User found and returned successfully.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal Server Error.</response>
        // GET api/<UserController>/user1
        [HttpGet("{login}")]
        public IActionResult Get(string login, UserType userType)
        {

            // Retrieve the user from the repository
            var user = _userRepoResolver(userType.ToString()).GetUser(login);

            if (user == null)
            {
                return NotFound("The requested user was not found");
            }

            switch (userType)
            {
                case UserType.JobSeeker:
                    {
                        UserJobSeekerModel seeker = _mapper.Map<UserJobSeekerModel>(user);
                        return Ok(seeker);
                    }
                case UserType.Hr:
                    {
                        UserHrModel hr = _mapper.Map<UserHrModel>(user);
                        return Ok(hr);
                    }

                default:
                    return BadRequest("Bad User type");

            }
        }

        // GET: api/<UserController
        /// <summary>
        /// Retrieves the list of all users.
        /// </summary>
        /// <returns>A collection of User objects.</returns>
        /// <response code="200">Returns the list of users successfully.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        [HttpGet("users")]
        public IActionResult GetUsers(UserType userType)
        {
            _logger.LogInformation("Get method start");
            // Retrieve users from the repository
            var users = _userRepoResolver(userType.ToString()).GetUsers();
            switch (userType)
            {
                case UserType.JobSeeker:
                    {
                        List<UserJobSeekerModel> seekers = _mapper.Map<List<UserJobSeekerModel>>(users);
                        return Ok(seekers);
                    }
                case UserType.Hr:
                    {
                        List<UserHrModel> hrs = _mapper.Map<List<UserHrModel>>(users);
                        return Ok(hrs);
                    }

                default:
                    return BadRequest("Bad User type");

            }
        }
        /// <summary>
        /// Add a new user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="201">The user was successfully created.</response>
        /// <response code="400">The user introduced has bad data format.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        // POST api/<UserController>
        [HttpPost]
        [SwaggerRequestExample(typeof(UserJobSeekerModel), typeof(UserExampleDocumentation))]
        [ProducesResponseType(typeof(UserJobSeekerModel), StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] UserModel user, [FromServices] AddUsersRequestValidator validator)
        {
            // 1. Validation
            var validationResult = validator.Validate(user);
            if (!validationResult.IsValid)
            {
                var firstError = validationResult.Errors[0];
                return firstError.ErrorCode switch
                {
                    ValidationErrorCodes.UserConflict => Conflict(new { message = firstError.ErrorMessage }),
                    ValidationErrorCodes.SkillsNotFound => NotFound(new { message = firstError.ErrorMessage }),
                    _ => BadRequest("Invalid input. Please verify your data and try again.")
                };
            }

            // 2. Logic (Only if valid)
            User userEntity = user.GetUserEntiy(_mapper);

            if (!string.IsNullOrEmpty(user.PasswordHash))
            {
                byte[] hashBytes = Encoding.UTF8.GetBytes(user.PasswordHash);
                userEntity.PasswordHash = Convert.ToBase64String(hashBytes);
            }

            UserType userType = (user is UserJobSeekerModel) ? UserType.JobSeeker : UserType.Hr;
            _userRepoResolver(userType.ToString()).AddUser(userEntity);

            var result = userEntity.GetUserModel(_mapper);
            return CreatedAtAction(nameof(Post), new { id = userEntity.Id }, result);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id, UserType type)
        {
            _userRepoResolver(type.ToString()).DeleteUser(id);
            return Ok();
        }
    }
}
