using CandidateTask.Application.IServices;
using CandidateTask.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CandidateTask.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(CandidateDto candidateDto)
        {
            try
            {
               var result = await _candidateService.SaveAsync(candidateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
