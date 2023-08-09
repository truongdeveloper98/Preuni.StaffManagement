using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Authentications;
using PreUni.StaffManagement.Core.Utilities;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Email;
using PreUni.StaffManagement.Core.Interfaces.Services.Email;
using PreUni.StaffManagement.Core.CustomExcceptions;
using PreUni.StaffManagement.Core.Interfaces.Services.Users;
using PreUni.StaffManagement.Core.Interfaces.JobApplications;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.Interfaces.Services.Interviews;
using Microsoft.AspNetCore.Identity;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;

namespace PreUni.StaffManagement.Web.Controllers;

public class RegistrationWebHookController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly PreUni.StaffManagement.Core.Interfaces.Authentications.IAuthenticationService _authenticationService;
    private readonly IEmailSenderService _emailSender;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IJobAppicationService _jobApplicationService;

	public RegistrationWebHookController(ILogger<AccountController> logger,
       IConfiguration configuration, PreUni.StaffManagement.Core.Interfaces.Authentications.IAuthenticationService authenticationService,
       IUserService userService,
       IEmailSenderService emailSender,
       IWebHostEnvironment webHostEnvironment,
       IJobAppicationService jobApplicationService)
    {
        _logger = logger;
        _configuration = configuration;
        _authenticationService = authenticationService;
        _userService = userService;
        _emailSender = emailSender;
        _webHostEnvironment = webHostEnvironment;
        _jobApplicationService = jobApplicationService;
    }
   

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<ResponseModel>> Register([FromBody]NewUserRequestViewModel request)
    {
        try
        {
            // create password random
            var password = RandomPassword.RandomString();

            var registerModel = new RegisterRequestViewModel()
                                       {
                                            FirstName =request.FirstName,
                                            LastName = request.LastName,
                                            Email= request.Email,
                                            Password =  password,
                                            PhoneNumber = request.PhoneNumber
                                        };

            // create new user from web hook
            var token = await _authenticationService.RegisterAsync(registerModel);
            registerModel.IdentityUser = token.Id;
            var result = await _userService.CreateNewUser(registerModel);
            
            if(result.Data is null)
               throw new RegistrationException(new IdentityError[] { new IdentityError() { Code= "0", Description="User is not exists" }});

            var modelCreateJob = new JobApplicationViewModel()
            {
                JobTitle = request.JobTitle,
                UserId = result.Data is null ? 0 : result.Data.Id
            };

            var resultCreateJob = await _jobApplicationService.Create(modelCreateJob);


            var fullName = request.FirstName + " " + request.LastName;

            // if create user success => send user,password to email
            await SendAccountToEmail(request.Email, password, fullName);
            return new ResponseModel()
            {
                Status = 1,
                Message = "Create new user successful"
            };
        }
        catch(RegistrationException ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, (ex as RegistrationException).Errors);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong please try again");
        }
        
    }

	private async Task SendAccountToEmail(string email, string password, string fullName)
	{
		await Task.Yield();
		string subject = "Account Login";
		string content = "";

        string path = _webHostEnvironment.ContentRootPath;
		
		var filePath = Path.Combine(path, "EmailTemplates\\SendEmail.html");
		using (StreamReader reader = new(filePath))
		{
			content = reader.ReadToEnd();
		}
        var baseUrl = _configuration["BaseUrl"];
		content = content.Replace("{UserNameTitle}", fullName);
		content = content.Replace("{Password}", password);
        content = content.Replace("{LoginLink}", $"{baseUrl}/Account/Login?email={email}");

		var message = new Message(new string[] { email }, subject, content);
		_emailSender.SendEmail(message);
	}
}

