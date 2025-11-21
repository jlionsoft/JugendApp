// JugendApp.Api/Controllers/AuthController.cs
using AutoMapper;
using JugendApp.Api.Auth;
using JugendApp.Api.Data;
using JugendApp.Api.Identity;
using JugendApp.DTOs;
using JugendApp.DTOs.Auth;
using JugendApp.SharedModels;
using JugendApp.SharedModels.Person;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JugendApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApiDBContext _context;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ApiDBContext context,
        ITokenService tokenService,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResultDto>> Register(RegisterDto dto)
    {
        var person = new Person(firstname: dto.Firstname, lastname: dto.Lastname, [], []);
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();

        var user = new ApplicationUser
        {
            UserName = dto.Username,
            PersonId = person.Id
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });

        // Standardrolle vergeben (z. B. Member)
        await _userManager.AddToRoleAsync(user, "Member");

        var token = await _tokenService.CreateTokenAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new AuthResultDto
        {
            Token = token,
            UserId = user.Id,
            PersonId = person.Id,
            Username = user.UserName!,
            Roles = roles
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResultDto>> Login(LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.Username);
        if (user == null) return Unauthorized();

        var signIn = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!signIn.Succeeded) return Unauthorized();

        var token = await _tokenService.CreateTokenAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new AuthResultDto
        {
            Token = token,
            UserId = user.Id,
            PersonId = user.PersonId,
            Username = user.UserName!,
            Roles = roles
        });
    }

    [HttpPost("create-user-for-person/{personId}")]
    public async Task<ActionResult<AuthResultDto>> CreateUserForPerson(
        int personId,
        [FromBody] RegisterDto dto,
        [FromServices] UserManager<ApplicationUser> userManager,
        [FromServices] ITokenService tokenService)
    {
        var person = await _context.Persons.FindAsync(personId);
        if (person == null) return NotFound("Person not found");

        var user = new ApplicationUser
        {
            UserName = dto.Username,
            PersonId = person.Id
        };

        var result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        // Standardrolle vergeben
        await userManager.AddToRoleAsync(user, "Member");

        var token = await tokenService.CreateTokenAsync(user);
        var roles = await userManager.GetRolesAsync(user);

        return new AuthResultDto
        {
            Token = token,
            UserId = user.Id,
            PersonId = person.Id,
            Username = user.UserName!,
            Roles = roles
        };
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<AuthResultDto>> Me()
    {
        // UserId aus den Claims holen
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound("User not found");

        // Person laden
        var person = await _context.Persons.FindAsync(user.PersonId);

        // Rollen laden
        var roles = await _userManager.GetRolesAsync(user);

        return new AuthResultDto
        {
            UserId = user.Id,
            PersonId = user.PersonId,
            Username = user.UserName!,
            Roles = roles,
            Token = null
        };
    }


    [HttpPost("create-role")]
    public async Task<IActionResult> CreateRole(string roleName, [FromServices] RoleManager<IdentityRole<int>> roleManager)
    {
        if (await roleManager.RoleExistsAsync(roleName))
            return BadRequest("Role already exists");

        var result = await roleManager.CreateAsync(new IdentityRole<int>(roleName));
        return result.Succeeded ? Ok() : BadRequest(result.Errors);
    }

    [HttpPost("add-role/{userId}")]
    public async Task<IActionResult> AddRole(int userId, [FromBody] string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return NotFound("User not found");

        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok($"Role '{roleName}' added to user {user.UserName}");
    }

    [HttpPost("remove-role/{userId}")]
    public async Task<IActionResult> RemoveRole(int userId, [FromBody] string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return NotFound("User not found");

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok($"Role '{roleName}' removed from user {user.UserName}");
    }

}