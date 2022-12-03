using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using UserIdentityStore.API.DataLayer.Models;

namespace UserIdentityStore.API.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserRepo(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ResponseModel> AdminRegister(RegisterModel model)
        {
            var userExist = await _userManager.FindByNameAsync(model.UserName);
            if (userExist != null)
            {
                return new ResponseModel()
                {
                    StatusCode = StatusCodes.Status406NotAcceptable,
                    Status = "Error",
                    Message = "User already exists!"
                };
            }
            AppUser user = new AppUser()
            {
                Email = model.Email,
                CreatedAt = DateTime.Now.ToString("dd/MMMM/yyyy hh:mm:ss"),
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                List<string> los = new List<string>();
                var errorList = result.Errors.ToList();
                foreach (var item in errorList)
                {
                    los.Add(item.Description);
                }

                return new ResponseModel()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = $"User creation failed due to {String.Concat(los)}"
                };
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            return new ResponseModel
            {
                StatusCode = StatusCodes.Status201Created,
                Status = "Success",
                Message = "Admin Created Successfully"
            };
        }

        public async Task<AuthResult> UserLogin(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRole = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim("userName",user.UserName),
                    new Claim("userId", user.Id.ToString()),
                    new Claim("firstName", user.FirstName),
                    new Claim("lastName", user.LastName),
                    new Claim("address", user.Address),
                    new Claim("mobile", user.PhoneNumber),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("createdAt",user.CreatedAt),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };
                foreach (var userrolw in userRole)
                {
                    authClaims.Add(new Claim("Role", userrolw));
                }
                var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Sign"]));
                var token = new JwtSecurityToken(
                            issuer: _configuration["JWT:ValidIssuer"],
                            audience: _configuration["JWT:ValidAudience"],
                            expires: DateTime.Now.AddMinutes(15),
                            claims: authClaims,
                             signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
                             );
                return new AuthResult
                {
                    Result = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }
            return new AuthResult
            {
                Result = false,
                Errors = new List<string>()
                {
                    "Invalid Credentials"
                }
            };
        }

        public async Task<ResponseModel> UserRegister(RegisterModel model)
        {
            var userExist = await _userManager.FindByNameAsync(model.UserName);
            if (userExist != null)
            {
                return new ResponseModel()
                {
                    StatusCode = StatusCodes.Status406NotAcceptable,
                    Status = "Error",
                    Message = "User already exists!"
                };
            }
            AppUser user = new AppUser()
            {
                Email = model.Email,
                CreatedAt = DateTime.Now.ToString("dd/MMMM/yyyy hh:mm:ss"),
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                List<string> los = new List<string>();
                var errorList = result.Errors.ToList();
                foreach (var item in errorList)
                {
                    los.Add(item.Description);
                }

                return new ResponseModel()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = $"User creation failed due to {String.Concat(los)}"
                };
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            return new ResponseModel
            {
                StatusCode = StatusCodes.Status201Created,
                Status = "Success",
                Message = "User Created Successfully"
            };
        }

    }
}

