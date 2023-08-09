using System.Diagnostics;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PreUni.StaffManagement.Core.Interfaces;
using PreUni.StaffManagement.Core.Interfaces.Services.Interviews;
using PreUni.StaffManagement.Core.Interfaces.Services.Users;
using PreUni.StaffManagement.Core.Services.Validator;
using PreUni.StaffManagement.Core.Utilities;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.User;
using PreUni.StaffManagement.Core.ViewModels.ValidateViewModels;
using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Web.Models;

namespace PreUni.StaffManagement.Web.Controllers;
[Authorize]
public class JobApplicationController : Controller
{

    private readonly ILogger<JobApplicationController> _logger;
    private readonly IConfiguration _configuration;
    private readonly ITFNDeclareService _tfnDeclareService;
    private readonly IBankAccountService _bankAccountService;
    private readonly IJobAppicationService _jobAppicationService;
    private readonly IParentConsentService _parentConsentService;
    private readonly IUserService _userService;
    private readonly ISuperAnnuationService _superAnnuationService;
    private readonly FormValidationService _formValidationService;

    public JobApplicationController(ILogger<JobApplicationController> logger
        , IConfiguration configuration,
        ITFNDeclareService tfnDeclareService,
        IBankAccountService bankAccountService,
        IJobAppicationService jobAppicationService,
        IParentConsentService parentConsentService,
        IUserService userService,
        ISuperAnnuationService superAnnuationService,
        FormValidationService formValidationService
        )
    {
        _logger = logger;
        _configuration = configuration;
        _tfnDeclareService = tfnDeclareService;
        _bankAccountService = bankAccountService;
        _superAnnuationService = superAnnuationService;
        _jobAppicationService = jobAppicationService;
        _parentConsentService = parentConsentService;
        _userService = userService;
        _formValidationService = formValidationService;
    }


    public async Task<IActionResult> TFNDeclare()
    {
        await GetJobApplicationId();
        return View();
    }
    public async Task<IActionResult> BankAccountDetail()
    {
        await GetJobApplicationId();
        return View();
    }
    public async Task<IActionResult> SuperAnnuation()
    {
        await GetJobApplicationId();
        return View();
    }
    public async Task<IActionResult> ParentConsent()
    {
        await GetJobApplicationId();
        return View();
    }
    public async Task<IActionResult> IdentityProoImage()
    {
        await GetJobApplicationId();
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<ResponseModel>> SubmitTFNDeclare([FromBody] TFNDeclare request)
    {
        try
        {
            // validate model before create new tfn declare
            ValidationResult validateResult = await _formValidationService.ValidateTFNDeclare(request);

            if (!validateResult.IsValid)
            {
                //if validate false add modelState error messages
                validateResult.AddToModelState(this.ModelState);
                var error = new
                {
                    Status = 0,
                    Errors = validateResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToList()
                            )
                };
                string jsonString = JsonConvert.SerializeObject(error);
                // return statusCode 400
                return BadRequest(jsonString);
            }
            // call service create new tfn declare
            var result = await _tfnDeclareService.Create(request);
            // if create new tfn declare success => return response model include status and message
            return new ResponseModel()
            {
                Status = 1,
                Message = result.Message
            };

        }
        catch (Exception ex)
        {
            // if falsed return statusCode 400 with message
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public async Task<ActionResult<ResponseModel>> SubmitBankAccount([FromBody] BankAccountDetail request)
    {
        try
        {
            // check validate before create new bank account detail
            ValidationResult validateResult = await _formValidationService.ValidateBankAccountDetail(request);
            if (!validateResult.IsValid)
            {
                //if validate failse add modelState with error messages
                validateResult.AddToModelState(this.ModelState);
                var error = new
                {
                    Status = 0,
                    Errors = validateResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToList()
                            )
                };
                string jsonString = JsonConvert.SerializeObject(error);
                //return statusCode 400 with array error messages
                return BadRequest(jsonString);
            }
            else
            {
                // call service create new bank account
                var result = await _bankAccountService.Create(request);

                // if success return new ResponseModel with status = 1
                return new ResponseModel()
                {
                    Status = result.Status,
                    Message = result.Message
                };
            }
        }
        catch (Exception ex)
        {
            // if create bank accoutn detail false => return status code 400 with message

            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ResponseModel>> SubmitParentConsent([FromBody] ParentConsent request)
    {

        try
        {
            // validate form befor create new parent consent
            ValidationResult validateResult = await _formValidationService.ValidateParentConsent(request);
            if (!validateResult.IsValid)
            {
                // if validate false, add modelState with error messages
                validateResult.AddToModelState(this.ModelState);
                var error = new
                {
                    Status = 0,
                    Errors = validateResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToList()
                            )
                };
                string jsonString = JsonConvert.SerializeObject(error);
                // return statusCode 400 with array error messages
                return BadRequest(jsonString);
            }
            else
            {
                // call service create new parent consent
                var result = await _parentConsentService.Create(request);

                //if success return new ResponseModel with status = 1
                return new ResponseModel()
                {
                    Status = 1,
                    Message = result.Message
                };
            }
        }
        catch (Exception ex)
        {
            // if false return status code 400 with error message
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ResponseModel>> SubmitProoImage(int jobApplicationId, IFormFile imageFile)
    {
        try
        {
            // check model state
            if (!ModelState.IsValid)
            {
                // if modelState false return status code 400 with  error message 
                return BadRequest("Image cannot be empty");
            }
            // get jobApplication
            var jobApplication = await _jobAppicationService.GetByJobId(jobApplicationId);
            // upload image with userId and path image
            var result = await _userService.UpdateImage(jobApplication.UserId, imageFile);
            // return new responseModel with status and message
            return new ResponseModel()
            {
                Status = result.Status,
                Message = result.Message
            };
        }
        catch (Exception ex)
        {
            // if upload image false return status code 400 with error message
            return BadRequest(ex.Message);
        }
    }
    [HttpGet]
    public async Task<ResponseModel<TFNDeclare>> GetTFNDeclareDetail(int jobApplicationId)
    {
        try
        {
            // get tfn declare by jobapplicationId
            var result = await _tfnDeclareService.GetByJobId(jobApplicationId);
            // return responseModel TFNDeclare with Status,Message and Data
            return new ResponseModel<TFNDeclare>
            {
                Status = result.Status,
                Message = result.Message,
                Data = result.Data
            };
        }
        catch (Exception)
        {
            // if tfn declare not found => return new responseModel tfnDeclare with status = 0,message and data = null
            return new ResponseModel<TFNDeclare>()
            {
                Status = 0,
                Message = "Get Bank Account False",
                Data = new TFNDeclare()
            };
        }
    }

    [HttpGet]
    public async Task<ResponseModel<BankAccountDetail>> GetBankAccountDetail(int jobApplicationId)
    {
        try
        {
            // get bank account by jobApplicationId
            var result = await _bankAccountService.GetByJobId(jobApplicationId);
            // return new responseModel bank account detail with status,message and data
            return new ResponseModel<BankAccountDetail>
            {
                Status = result.Status,
                Message = result.Message,
                Data = result.Data
            };
        }
        catch (Exception)
        {
            // if bank account not found => return new responseModel bank account with status = 0,message and data = null
            return new ResponseModel<BankAccountDetail>()
            {
                Status = 0,
                Message = "Get Bank Account False",
                Data = new BankAccountDetail()
            };
        }


    }

    [HttpGet]
    public async Task<ResponseModel<UserImageResponse>> GetProoImageDetail(int jobApplicationId)
    {
        try
        {
            // get jobapplication by Id
            var resultJob = await _jobAppicationService.GetByJobId(jobApplicationId);

            // get image user by jobapplicationId
            var result = await _userService.GetImageUserById(resultJob.UserId);

            // return new responseModel userImageResponse with status, message and data
            return new ResponseModel<UserImageResponse>()
            {
                Status = result.Status,
                Message = "Get Image User Success",
                Data = result.Data
            };
        }
        catch (Exception)
        {
            // if get image user false return new responseModel with status,Message and Data = null
            return new ResponseModel<UserImageResponse>()
            {
                Status = 0,
                Message = "Get Image User False",
                Data = new UserImageResponse()
            };
        }
    }

    [HttpGet]
    public async Task<ActionResult<ResponseModel<ParentConsent>>> GetParentConsentDetail(int jobApplicationId)
    {
        try
        {
            // get parent consent by jobapplicationId
            var result = await _parentConsentService.GetByJobId(jobApplicationId);

            // return new responseMode parent consent with Status,message and data
            return new ResponseModel<ParentConsent>()
            {
                Status = result.Status,
                Message = "Get Image User Success",
                Data = result.Data
            };
        }
        catch (Exception e)
        {
            // if get parent consent false return new responseModel Parent consent with status, message and data = null
            return new ResponseModel<ParentConsent>()
            {
                Status = 0,
                Message = e.Message,
                Data = new ParentConsent()
            };
        }
    }

    [HttpPost]
    public async Task<ActionResult<ResponseModel>> UpdateTFNDeclare([FromBody] TFNDeclare request, int jobApplicationId)
    {
        try
        {
            // check validate before update data
            ValidationResult validateResult = await _formValidationService.ValidateTFNDeclare(request);
            if (!validateResult.IsValid)
            {
                // if validate false add model state
                validateResult.AddToModelState(this.ModelState);
                var error = new
                {
                    Status = 0,
                    Errors = validateResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToList()
                            )
                };
                string jsonString = JsonConvert.SerializeObject(error);
                // return status code 400 with error message
                return BadRequest(jsonString);
            }
            // update data with jobapplicationId
            var result = await _tfnDeclareService.Update(request, jobApplicationId);

            //if update data success return new responseModel with status and message
            return new ResponseModel()
            {
                Status = result.Status,
                Message = result.Message,
            };
        }
        catch (Exception ex)
        {
            // if update data false return status code 400 with error message
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ResponseModel>> UpdateBankAccountDetail([FromBody] BankAccountDetail request, int jobApplicationId)
    {
        try
        {
            // check validate before update data
            ValidationResult validateResult = await _formValidationService.ValidateBankAccountDetail(request);
            if (!validateResult.IsValid)
            {
                // if validate false add model state
                validateResult.AddToModelState(this.ModelState);
                var error = new
                {
                    Status = 0,
                    Errors = validateResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToList()
                            )
                };
                string jsonString = JsonConvert.SerializeObject(error);
                // return status code 400 with error message
                return BadRequest(jsonString);
            }
            // update data with jobapplicationId
            var result = await _bankAccountService.Update(request, jobApplicationId);

            //if update data success return new responseModel with status and message
            return new ResponseModel()
            {
                Status = result.Status,
                Message = result.Message,
            };

        }
        catch (Exception ex)
        {
            // if update data false return status code 400 with error message
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ResponseModel>> UpdateParentConsent([FromBody] ParentConsent request, int jobApplicationId)
    {
        try
        {
            // check validate before update data
            ValidationResult validateResult = await _formValidationService.ValidateParentConsent(request);
            if (!validateResult.IsValid)
            {
                // if validate false add model state
                validateResult.AddToModelState(this.ModelState);
                var error = new
                {
                    Status = 0,
                    Errors = validateResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToList()
                            )
                };
                string jsonString = JsonConvert.SerializeObject(error);

                //if update data success return new responseModel with status and message
                return BadRequest(jsonString);
            }
            // update data with jobapplicationId
            var result = await _parentConsentService.Update(request, jobApplicationId);

            //if update data success return new responseModel with status and message
            return new ResponseModel()
            {
                Status = result.Status,
                Message = result.Message,
            };

        }
        catch (Exception e)
        {
            // if update data false return status code 400 with error message
            return BadRequest(e.Message);
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

    [HttpGet]
    public async Task<ActionResult<ResponseModel<SuperAnnuation>>> GetSuperAnnuationDetail(int jobApplicationId)
    {
        try
        {
            var result = await _superAnnuationService.GetOne(jobApplicationId);
            return new ResponseModel<SuperAnnuation>()
            {
                Status = 1,
                Message = "Get SuperAnnuation successfull",
                Data = result
            }; ;
        }
        catch
        {
            return new ResponseModel<SuperAnnuation>()
            {
                Status = 0,
                Message = "Get SuperAnnuation error",
                Data = null
            };
        }
    }
    [HttpPost]
    public async Task<ActionResult> SubmitSuperAnnuation([FromBody] SuperAnnuation request)
    {
        try
        {
            if (request.SuperAnnuationType == "undefined")
            {
                ValidateYourDetails requestYourDetails = new ValidateYourDetails()
                {
                    SuperAnnuationType = null,
                    JobApplicationId = request.JobApplicationId,
                    FullName = request.FullName,
                    EmployeeNumber = request.EmployeeNumber,
                    TaxFileNumber = request.TaxFileNumber
                };
                ValidationResult validationYourDetails = await _formValidationService.ValidateYourDetails(requestYourDetails);
                if (!validationYourDetails.IsValid)
                {
                    validationYourDetails.AddToModelState(this.ModelState);
                    var error = new
                    {
                        Status = 0,
                        Errors = validationYourDetails.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToList()
                            )
                    };
                    string jsonString = JsonConvert.SerializeObject(error);
                    return BadRequest(jsonString);
                }
                var result = await _superAnnuationService.Create(request);
                return Ok(result);
            }
            if (request.SuperAnnuationType == "1")
            {
                ValidateSuperFundDetails requestSuperFundDetails = new ValidateSuperFundDetails()
                {
                    SuperAnnuationType = request.SuperAnnuationType,
                    JobApplicationId = request.JobApplicationId,
                    FullName = request.FullName,
                    EmployeeNumber = request.EmployeeNumber,
                    TaxFileNumber = request.TaxFileNumber,
                    FundName = request.FundName,
                    FundBusinessNumber = request.FundBusinessNumber,
                    FundAccountNumber = request.FundAccountNumber,
                    FundAppearName = request.FundAppearName,
                    FundAttachLetter = request.FundAttachLetter,
                    FundSignature = request.FundSignature,
                    DateFundSignature = request.DateFundSignature,
                    FundAnnuationIdentifi = request.FundAnnuationIdentifi,
                };
                ValidationResult validateSuperFundDetails = await _formValidationService.ValidateSuperFundDetails(requestSuperFundDetails);
                if (!validateSuperFundDetails.IsValid)
                {
                    validateSuperFundDetails.AddToModelState(this.ModelState);
                    var error = new
                    {
                        Status = 0,
                        Errors = validateSuperFundDetails.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToList()
                            )
                    };
                    string jsonString = JsonConvert.SerializeObject(error);
                    return BadRequest(jsonString);
                }
                var result = await _superAnnuationService.Create(request);
                return Ok(result);
            }
            if (request.SuperAnnuationType == "2")
            {
                ValidateBusinessDetails requestBusinessDetails = new ValidateBusinessDetails()
                {
                    SuperAnnuationType = request.SuperAnnuationType,
                    JobApplicationId = request.JobApplicationId,
                    FullName = request.FullName,
                    EmployeeNumber = request.EmployeeNumber,
                    TaxFileNumber = request.TaxFileNumber,
                    BusinessName = request.BusinessName,
                    BusinessNumber = request.BusinessNumber,
                    BusinessFundName = request.BusinessFundName,
                    BusinessFundNumber = request.BusinessFundNumber,
                    BusinessAnnuationIdentifi = request.BusinessAnnuationIdentifi,
                    BusinessNewAccount = request.BusinessNewAccount,
                    SignatureEmloyee = request.SignatureEmloyee,
                    DateSignatureEmloyee = request.DateSignatureEmloyee
                };
                ValidationResult validateBusinessDetails = await _formValidationService.ValidateBusinessDetails(requestBusinessDetails);
                if (!validateBusinessDetails.IsValid)
                {
                    validateBusinessDetails.AddToModelState(this.ModelState);
                    var error = new
                    {
                        Status = 0,
                        Errors = validateBusinessDetails.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToList()
                            )
                    };
                    string jsonString = JsonConvert.SerializeObject(error);
                    return BadRequest(jsonString);
                }
                var result = await _superAnnuationService.Create(request);
                return Ok(result);
            }
            if (request.SuperAnnuationType == "3")
            {
                ValidateSmfsDetails requestSmfsDetails = new ValidateSmfsDetails()
                {
                    SuperAnnuationType = request.SuperAnnuationType,
                    JobApplicationId = request.JobApplicationId,
                    FullName = request.FullName,
                    EmployeeNumber = request.EmployeeNumber,
                    TaxFileNumber = request.TaxFileNumber,
                    SmsfName = request.SmsfName,
                    SmsfNumber = request.SmsfNumber,
                    SmsfAppearAccount = request.SmsfAppearAccount,
                    SmsfServiceAddress = request.SmsfServiceAddress,
                    BankAccountName = request.BankAccountName,
                    BsbCode = request.BsbCode,
                    SmsfAcountNumber = request.SmsfAcountNumber,
                    HaveAtoSmsf = request.HaveAtoSmsf,
                    SignatureSmsf = request.SignatureSmsf,
                    DateSignatureSmsf = request.DateSignatureSmsf
                };
                ValidationResult validateSmfsDetails = await _formValidationService.ValidateSmfsDetails(requestSmfsDetails);
                if (!validateSmfsDetails.IsValid)
                {
                    validateSmfsDetails.AddToModelState(this.ModelState);
                    var error = new
                    {
                        Status = 0,
                        Errors = validateSmfsDetails.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToList()
                            )
                    };
                    string jsonString = JsonConvert.SerializeObject(error);
                    return BadRequest(jsonString);
                }
                var result = await _superAnnuationService.Create(request);
                return Ok(result);
            }
            return BadRequest("Super Annuation not created");
        }
        catch
        {
            return BadRequest("Error");
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateSuperAnnuation([FromRoute] int id, [FromBody] SuperAnnuation request)
    {
        try
        {
            if (request.SuperAnnuationType == null)
            {
                var errors = new
                {
                    Status = 4,
                    Error = "SuperAnnuationType is required"
                };
                return BadRequest(errors);
            }
            if (request.SuperAnnuationType == "1")
            {
                ValidateSuperFundDetails requestSuperFundDetails = new ValidateSuperFundDetails()
                {
                    SuperAnnuationType = request.SuperAnnuationType,
                    JobApplicationId = request.JobApplicationId,
                    FullName = request.FullName,
                    EmployeeNumber = request.EmployeeNumber,
                    TaxFileNumber = request.TaxFileNumber,
                    FundName = request.FundName,
                    FundBusinessNumber = request.FundBusinessNumber,
                    FundAccountNumber = request.FundAccountNumber,
                    FundAppearName = request.FundAppearName,
                    FundAttachLetter = request.FundAttachLetter,
                    FundSignature = request.FundSignature,
                    DateFundSignature = request.DateFundSignature,
                    FundAnnuationIdentifi = request.FundAnnuationIdentifi,
                };
                ValidationResult validateSuperFundDetails = await _formValidationService.ValidateSuperFundDetails(requestSuperFundDetails);
                if (!validateSuperFundDetails.IsValid)
                {
                    validateSuperFundDetails.AddToModelState(this.ModelState);
                    var error = new
                    {
                        Status = 0,
                        Errors = validateSuperFundDetails.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToList()
                            )
                    };
                    string jsonString = JsonConvert.SerializeObject(error);
                    return BadRequest(jsonString);
                }
                var result = await _superAnnuationService.Update(id,request);
                return Ok(result);
            }
            if (request.SuperAnnuationType == "2")
            {
                ValidateBusinessDetails requestBusinessDetails = new ValidateBusinessDetails()
                {
                    SuperAnnuationType = request.SuperAnnuationType,
                    JobApplicationId = request.JobApplicationId,
                    FullName = request.FullName,
                    EmployeeNumber = request.EmployeeNumber,
                    TaxFileNumber = request.TaxFileNumber,

                    BusinessName = request.BusinessName,
                    BusinessNumber = request.BusinessNumber,
                    BusinessFundName = request.BusinessFundName,
                    BusinessFundNumber = request.BusinessFundNumber,
                    BusinessAnnuationIdentifi = request.BusinessAnnuationIdentifi,
                    BusinessNewAccount = request.BusinessNewAccount,
                    SignatureEmloyee = request.SignatureEmloyee,
                    DateSignatureEmloyee = request.DateSignatureEmloyee
                };
                ValidationResult validateBusinessDetails = await _formValidationService.ValidateBusinessDetails(requestBusinessDetails);
                if (!validateBusinessDetails.IsValid)
                {
                    validateBusinessDetails.AddToModelState(this.ModelState);
                    var error = new
                    {
                        Status = 0,
                        Errors = validateBusinessDetails.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToList()
                            )
                    };
                    string jsonString = JsonConvert.SerializeObject(error);
                    return BadRequest(jsonString);
                }
                var result = await _superAnnuationService.Update(id,request);
                return Ok(result);
            }
            if (request.SuperAnnuationType == "3")
            {
                ValidateSmfsDetails requestSmfsDetails = new ValidateSmfsDetails()
                {
                    SuperAnnuationType = request.SuperAnnuationType,
                    JobApplicationId = request.JobApplicationId,
                    FullName = request.FullName,
                    EmployeeNumber = request.EmployeeNumber,
                    TaxFileNumber = request.TaxFileNumber,
                    SmsfName = request.SmsfName,
                    SmsfNumber = request.SmsfNumber,
                    SmsfAppearAccount = request.SmsfAppearAccount,
                    SmsfServiceAddress = request.SmsfServiceAddress,
                    BankAccountName = request.BankAccountName,
                    BsbCode = request.BsbCode,
                    SmsfAcountNumber = request.SmsfAcountNumber,
                    HaveAtoSmsf = request.HaveAtoSmsf,
                    SignatureSmsf = request.SignatureSmsf,
                    DateSignatureSmsf = request.DateSignatureSmsf
                };
                ValidationResult validateSmfsDetails = await _formValidationService.ValidateSmfsDetails(requestSmfsDetails);
                if (!validateSmfsDetails.IsValid)
                {
                    foreach (var error in validateSmfsDetails.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    string jsonString = JsonConvert.SerializeObject(ModelState);
                    return BadRequest(jsonString);
                }
                var result = await _superAnnuationService.Update(id,request);
                return Ok(result);
            }
            return BadRequest("Super Annuation not update");
        }
        catch
        {
            return BadRequest("Error");
        }
    }

    [HttpGet]
    public IActionResult Complete()
    {
        return View();
    }

    private async Task<IActionResult> GetJobApplicationId()
    {
        try
        {
            if (!this.ControllerContext.HttpContext.User.Identity.IsAuthenticated)
                throw new Exception();

            // get data from session when user login
            var identityName = this.ControllerContext.HttpContext.User.Identity.Name;

            ViewBag.Email = identityName;

            ViewBag.PhoneNumber = (await _userService.GetUserByUserName(identityName)).Data?.PhoneNumber;
            var userId = (await _userService.GetUserByUserName(identityName)).Data.Id;

            // check if jobapplication with userid exists
            var isJobExists = await _jobAppicationService.GetByUserId(userId);

            var jobApplicationId = 0;

            // if existed return view
            if (isJobExists.Data is not null)
            {
                jobApplicationId = isJobExists.Data.Id;
            }
            else
            {
                // call method create jobapplication from JobApplicationRespository
                var result = await _jobAppicationService.Create(new JobApplicationViewModel()
                {
                    UserId = userId,
                });

                jobApplicationId = result.Data is null ? -1 : result.Data.Id;
            }

            // pass userId to ViewBag
            ViewBag.JobApplicationId = jobApplicationId;

        }
        catch (Exception)
        {
            return RedirectToAction("Login", "Account");
        }

        return View();
    }
}

