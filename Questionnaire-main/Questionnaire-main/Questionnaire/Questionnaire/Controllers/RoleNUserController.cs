using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.Model.DbSet;
using Questionnaire.Model.Entity;

namespace Questionnaire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleNUserController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IMapper mapper) : ControllerBase
    {

        // Get all users
        [HttpGet("getallusers")]
        public IActionResult GetAllUsers()
        {
            var users = userManager.Users.Select(user => new {
                user.Id,
                user.Name,
                user.Email,
                user.PhoneNumber
            }).ToList();

            return Ok(users);
        }


        // Create a New Role
        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("Role name is required.");
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (roleExist)
                return BadRequest("Role already exists.");
            var result = await roleManager.CreateAsync(new IdentityRole { Name = roleName });
            if (result.Succeeded)
                return Ok($"Role '{roleName}' created successfully.");
            return BadRequest(result.Errors);
        }

        // Assign Role to a User
        [HttpPost("assign")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleRequest model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound("User not found.");
            var roleExists = await roleManager.RoleExistsAsync(model.Role);
            if (!roleExists)
                return NotFound("Role does not exist.");
            var result = await userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
                return Ok($"Role '{model.Role}' assigned to '{model.Email}'.");
            return BadRequest(result.Errors);
        }

        // Create User
        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUserDTO applicationUserdto)
        {
            PasswordHasher<IdentityUser> hasher = new();
            var user = await userManager.FindByEmailAsync(applicationUserdto.Email);
            if (user != null)
                return BadRequest("User Already exists.");
            var appUser = mapper.Map<ApplicationUser>(applicationUserdto);
            appUser.PasswordHash = hasher.HashPassword(appUser, applicationUserdto.Password);
            var result = await userManager.CreateAsync(appUser);
            if (result.Succeeded)
                return Ok($"User '{appUser.Name}' created successfully.");
            return BadRequest(result.Errors);
        }

        // Get All Roles
        [HttpGet("getallrole")]
        public IActionResult GetAllRoles()
        {
            var roles = roleManager.Roles.Select(r => r.Name);
            return Ok(roles);
        }

        // Get Roles and Id for a Specific User
        [HttpGet("userinfo")]
        public async Task<IActionResult> GetUserDetails(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            if (user == null)
                return NotFound("User not found.");
            var role = await userManager.GetRolesAsync(user);
            return Ok(new { user.Id, user.Name, user.PhoneNumber, role });
        }

        // ✅ NEW: Get Users by Role
        [HttpGet("getUsersByRole")]
        public async Task<IActionResult> GetUsersByRole([FromQuery] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("Role name is required.");

            var usersInRole = await userManager.GetUsersInRoleAsync(roleName);

            var result = usersInRole.Select(user => new
            {
                user.Id,
                user.Name,
                user.Email,
                user.PhoneNumber
            });

            return Ok(result);
        }
        [HttpGet("currentuser")]
        
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.Identity?.Name;
            if (email == null)
                return Unauthorized();

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("User not found.");

            var roles = await userManager.GetRolesAsync(user);

            return Ok(new { user.Id, user.Name, user.Email, user.PhoneNumber, roles });
        }

    }

    public class AssignRoleRequest
    {
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
    }
}
