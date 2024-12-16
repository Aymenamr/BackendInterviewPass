using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Models.User;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterviewPass.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IJobSeekerRepository _jobSeekerRepository;
        public UserController(ILogger<UserController> logger,IJobSeekerRepository userRepository)
        {
            _logger = logger;
            _jobSeekerRepository = userRepository;
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
        public IActionResult Get(string login)
        {
            var user = _jobSeekerRepository.GetUser(login);
           
            UserModel model = new UserJobSeekerModel()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Login = user.Login,
                Phone = user.Phone,
                UserType="JobSeeker"
            };

            return Ok(model);
        }

        // GET: api/<UserController
        /// <summary>
        /// Retrieves the list of all users.
        /// </summary>
        /// <returns>A collection of User objects.</returns>
        /// <response code="200">Returns the list of users successfully.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        [HttpGet("users")]
        public List<UserModel> GetUsers(string userType)
        {
            List<UserModel> usersModel = new List<UserModel>();
          
            var users= _jobSeekerRepository.GetUsers();
           
            foreach(var user in users)
            {
                usersModel.Add(
                    new UserJobSeekerModel {
                                Id = user.Id,
                                Name = user.Name,
                                Email = user.Email,
                                Login = user.Login,
                                Phone = user.Phone,
                                });
            }
            return usersModel;
        }
        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] UserModel user)
        {
            if (user is UserJobSeekerModel Jsker)
            {
                var userEntity = new UserJobSeeker()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = user.Email,
                    Login = user.Login,
                    Name = user.Name,
                    Phone = user.Phone,
                    Password = user.Password,
                    LevelOfExperience = Jsker.LevelOfExperience
                };
                _jobSeekerRepository.AddUser(userEntity);
                return CreatedAtAction(nameof(Post), new { id = userEntity.Id }, userEntity);
            }
            return BadRequest("Unable to detect the type of the user");
        }


        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _jobSeekerRepository.DeleteUser(id);
        }
    }
}
