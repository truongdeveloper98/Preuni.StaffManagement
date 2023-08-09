using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PreUni.StaffManagement.Core.Interfaces.Services.Interviews;
using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Core.Services.Validator;
using FluentValidation.Results;
using Newtonsoft.Json;
using PreUni.StaffManagement.Core.Utilities;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;

namespace PreUni.StaffManagement.AdminWeb.Controllers
{
    [Authorize]
    public class TFNDeclareController : Controller
    {
        private readonly ITFNDeclareService _tFNDeclareService;
        private readonly FormValidationService _formValidationService;

        public TFNDeclareController(ITFNDeclareService tFNDeclareService, FormValidationService formValidationService)
        {
            _tFNDeclareService = tFNDeclareService;
            _formValidationService = formValidationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTFNDeclareById(int jobApplicationId)
        {
            try
            {
                var result = await _tFNDeclareService.GetByJobId(jobApplicationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTFNDeclare([FromBody] TFNDeclare model, int jobApplicationId)
        {
            try
            {
                ValidationResult validateResult = await _formValidationService.ValidateTFNDeclare(model);
                if (!validateResult.IsValid)
                {
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
                    return BadRequest(jsonString);
                }
                
                var result = await _tFNDeclareService.Update(model, jobApplicationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}