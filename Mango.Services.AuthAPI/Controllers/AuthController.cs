using Azure;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDto _response;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }
        //add HTTP POST method register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var result = await _authService.Register(model);
            if (!string.IsNullOrEmpty(result) )
            {
                _response.IsSuccess = false;
                _response.Message = result;
                return BadRequest(_response);
            }
            else
            {
                _response.IsSuccess = true;
            }

            return Ok(_response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var login =  await _authService.Login(model);
            if (string.IsNullOrEmpty(login.Token) && login.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "username or password is incorrect";
            }
            _response.IsSuccess = true;
            _response.Response = login;

            return Ok(_response);
        }

        [HttpPost]
        [Route("assignrole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var result = await _authService.AssignRole(model.Email,model.Role);
            if (!result)
            {
                _response.IsSuccess = false;
                _response.Message = "Error Encounter";
                return BadRequest(_response);
            }
            else
            {
                _response.IsSuccess = true;
            }

            return Ok(_response);
        }
    }
}
