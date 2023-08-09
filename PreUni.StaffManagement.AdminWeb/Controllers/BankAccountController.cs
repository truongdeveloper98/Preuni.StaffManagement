using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PreUni.StaffManagement.Core.Interfaces.Services.Interviews;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Domain.Entities;

namespace PreUni.StaffManagement.AdminWeb.Controllers
{
    [Authorize]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;

        public BankAccountController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBankAccountById(int jobApplicationId)
        {
            try
            {
                var result = await _bankAccountService.GetByJobId(jobApplicationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBankAccount([FromBody] BankAccountDetail model, int jobApplicationId)
        {
            try
            {
                var result = await _bankAccountService.Update(model, jobApplicationId);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}