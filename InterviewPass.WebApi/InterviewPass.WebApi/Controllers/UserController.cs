﻿using InterviewPass.DataAccess.Repositories.Interfaces;
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
        private readonly IJobSeekerRepository _userRepository;
        public UserController(ILogger<UserController> logger,IJobSeekerRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
      
        /// <summary>
        /// Retrieve a User according to his Id
        /// </summary>
        /// <param name="login">the Login of the user</param>
        /// <returns></returns>
        /// <response code="200">User found and returned successfully.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal Server Error.</response>
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public UserModel Get(string id)
        {
            var user = _userRepository.GetUser(id);
           
            UserModel model = new UserModel()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Login = user.Login,
                Phone = user.Phone,
            };

            return model;
        }

        // GET: api/<UserController
        /// <summary>
        /// Retrieves the list of all users.
        /// </summary>
        /// <returns>A collection of User objects.</returns>
        /// <response code="200">Returns the list of users successfully.</response>
        /// <response code="500">If there is an error retrieving the data.</response>
        [HttpGet]
        public List<UserModel> Get()
        {
            List<UserModel> usersModel = new List<UserModel>();
          
            var users= _userRepository.GetUsers();
           
            foreach(var user in users)
            {
                usersModel.Add(
                    new UserModel {
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
        public void Post([FromBody] UserModel user)
        {
            if (user is UserJobSeekerModel)
            {
                //_userRepository.AddUser(user);
            }
        }

        
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _userRepository.DeleteUser(id);
        }
    }
}