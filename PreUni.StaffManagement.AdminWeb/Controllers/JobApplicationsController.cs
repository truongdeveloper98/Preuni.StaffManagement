using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreUni.StaffManagement.Core.Interfaces.JobApplications;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;

namespace PreUni.StaffManagement.AdminWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobApplicationService _jobApplicationService;
        public JobApplicationsController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        [HttpGet]
        [Route("/api/jobApplications")]
        public async Task<IActionResult> GetJobApplications([FromQuery] JobApplicationListRequestViewModel model)
        {
            try
            {
                var result = await _jobApplicationService.GetJobApplications(model.sortColumnName, model.textSearch != default ? model.textSearch : "", model.currentPage, model.pageSize, model.sortColumnDirection);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("/api/jobApplications/{id}")]
        public async Task<IActionResult> GetJobApplicationById([FromRoute] int id)
        {
            try
            {
                var result = await _jobApplicationService.GetJobApplicationById(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("/api/jobApplications")]
        public async Task<IActionResult> CreateJobApplication([FromBody] JobApplicationRequestModel model)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(model);
                var result = await _jobApplicationService.CreateJobApplicationManual(model);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("/api/jobApplications/{id}")]
        public async Task<IActionResult> UpdateJobApplication([FromBody] JobApplicationRequestModel model, int id)
        {
            try
            {
                var result = await _jobApplicationService.UpdateJobApplication(model, id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("/api/jobApplications/{id}")]
        public async Task<IActionResult> DeleteJobApplications([FromRoute] int id)
        {
            try
            {
                var result = await _jobApplicationService.DeleteJobApplication(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}