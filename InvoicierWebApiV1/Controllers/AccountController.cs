using InvoicierWebApiV1.Data.AuthModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;

namespace InvoicierWebApiV1.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AccountController : ControllerBase
    {
        //private readonly ILogger<AccountController> _logger;
        //private readonly IUserService _userService;
        //private readonly IJwtAuthManager _jwtAuthManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public AccountController(
         UserManager<ApplicationUser> userManager, 
         RoleManager<IdentityRole> roleManager, 
         IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }



        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var userExist = await userManager.FindByNameAsync(model.UserName);
                if (userExist != null)
                { 
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response
                        {
                            Message = "User Already Exists",
                            Status = "Error"
                        });
                }
                var role = new IdentityRole();
                role.Name = UserRoles.User;
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    EmailConfirmed = true,
                    UserRoleId = role.Id                    
                };

                var result = await userManager.CreateAsync(user, model.Password);


                if (!result.Succeeded)
                {
                    var response =  new Response
                        {
                            Message = "User Registration Failed",
                            Status = "Error"
                        };
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                       
                }
                
                {
                    return Ok(new Response
                    {
                        Message = "User Created Successfully",
                        Status = "Success"
                    });
                }
            }
            catch (System.Exception)
            {
                
                throw;
            }
            // return Ok(); 
        }
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            var passwordCheck = await userManager.CheckPasswordAsync(user, model.Password);
            if (user != null && passwordCheck)
            {
                var roles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                { 
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }; 
                foreach (var userRole in roles)
                { 
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                    );
                
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                }); 
            }
            return Unauthorized();
        }
        [AllowAnonymous]
        [HttpPost("registeradmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExist = await userManager.FindByNameAsync(model.UserName);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Message = "User Already Exists",
                        Status = "Error"
                    });
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Message = "User Registration Failed",
                        Status = "Error"
                    });
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))  
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));  
            if (!await roleManager.RoleExistsAsync(UserRoles.User))  
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));  
  
            if (await roleManager.RoleExistsAsync(UserRoles.Admin))  
            {  
                await userManager.AddToRoleAsync(user, UserRoles.Admin);  
            }  
            {
                return Ok(new Response
                {
                    Message = "User Created Successfully",
                    Status = "Success"
                });
            }
        }
    }


}
