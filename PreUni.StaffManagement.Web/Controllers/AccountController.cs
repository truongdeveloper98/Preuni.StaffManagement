using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreUni.StaffManagement.Web.Models;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Authentications;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using PreUni.StaffManagement.Core.Constants.Systems;
using PreUni.StaffManagement.Infrastructure.Services.Authentications;
using PreUni.StaffManagement.Core.Utilities;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Email;
using PreUni.StaffManagement.Core.Interfaces.Services.Email;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Core.Interfaces.Services.Users;
using Microsoft.Win32;

namespace PreUni.StaffManagement.Web.Controllers;
[Authorize]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly PreUni.StaffManagement.Core.Interfaces.Authentications.IAuthenticationService _authenticationService;
    private readonly IEmailSenderService _emailSender;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public AccountController(ILogger<AccountController> logger,
       IConfiguration configuration,
       PreUni.StaffManagement.Core.Interfaces.Authentications.IAuthenticationService authenticationService,
       IUserService userService,
       IEmailSenderService emailSender,
       IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _configuration = configuration;
        
        _authenticationService = authenticationService;
        _userService = userService;
        _emailSender = emailSender;
        _webHostEnvironment = webHostEnvironment;
    }


    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string email)
    {
        ViewBag.Email = email;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Login([FromBody] LoginRequestViewModel request)
    {
        if (!ModelState.IsValid)
            return View(request);
       
       // get token when user login in system
        try
        {
            var result = await _authenticationService.LoginAsync(request);
            return Ok("Login successful");   
        }
        catch
        {
            return BadRequest("Login failed");
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
	
}

