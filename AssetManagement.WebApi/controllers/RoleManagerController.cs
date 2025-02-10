using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Models.db;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.WebApi.controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/role-manager")]
    [ApiVersion("1.0")]
    public class RoleManagerController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleManagerController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet("getall-roles")]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }
        [HttpGet("role-exists/{roleName}")]
        public async Task<IActionResult> RoleExists(string roleName)
        {
            var exists = await _roleManager.RoleExistsAsync(roleName);
            return Ok(new { RoleExists = exists });
        }

        [HttpGet("get-role-byname/{roleName}")]
        public async Task<IActionResult> GetRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return NotFound("Role not found");

            return Ok(role);
        }
        [HttpGet("user-roles/{userId}")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found");

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        [HttpGet("users-in-role/{roleName}")]
        public async Task<IActionResult> GetUsersInRole(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            return Ok(users);
        }



    }
}