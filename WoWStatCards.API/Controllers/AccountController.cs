
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using WowStatCards.Models;
using WowStatCards.Models.Domain;
using WoWStatCards.API.DTO;
using WoWStatCards.API.Services;

namespace WoWStatCards.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;
        protected ApiResponse _response;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _response = new();
        }
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse>> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email.ToUpper());

                if (user == null)
                {
                    _response.StatusCode = HttpStatusCode.Unauthorized;
                    _response.IsSuccess = false;

                    return Unauthorized("Invalid Email");
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (result.Succeeded)
                {
                    _response.Result = CreateUserObject(user);
                    _response.IsSuccess = true;
                    _response.StatusCode = HttpStatusCode.OK;

                    return Ok(_response);
                }

                return Unauthorized("Invalid password");

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return Unauthorized(_response);
        }
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> Register(RegisterDto registerDto)
        {
            if (registerDto.UserName == "" || registerDto.Email == "")
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Username and Email cannot be blank." };

                return BadRequest(_response);
            }

            try
            {
                var users = _userManager.Users;
                if (await users.AnyAsync(x => x.UserName == registerDto.UserName))
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Username already taken." };

                    return BadRequest(_response);
                }
                if (await users.AnyAsync(x => x.Email == registerDto.Email))
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Email already taken." };

                    return BadRequest(_response);
                }

                var user = new User
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    DisplayName = registerDto.DisplayName,
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    var userDto = CreateUserObject(user);

                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = userDto;

                    return Ok(_response);
                }

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;

                var errorList = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Description);
                }
                _response.ErrorMessages = errorList;

                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<ApiResponse>> Me()
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Email);

                if (email == null)
                {
                    _response.StatusCode = HttpStatusCode.Unauthorized;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Problem finding current logged in user." };

                    return Unauthorized(_response);
                }

                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    _response.StatusCode = HttpStatusCode.Unauthorized;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Problem finding current logged in user." };

                    return Unauthorized(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = CreateUserObject(user);

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        private UserDto CreateUserObject(User user)
        {
            return new UserDto
            {
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
            };
        }
    }
}
