using CVManager.Core.DTOs;
using CVManager.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CVManager.API.Controllers
{
    [ApiController]
    [Route("api/cvManager")]
    public class CvManagerController : ControllerBase
    {
        private readonly ICvManagerService _cvManagerService;
        public CvManagerController(ICvManagerService cvManagerService)
        {
             _cvManagerService = cvManagerService;
        }

        [HttpGet("GetCvsList")]
        public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize)
        {
            var items = await _cvManagerService.GetCvsListAsync(pageNumber, pageSize);

            var pagedCvs = new PagedList<CvDTO>
            {
                Items = items,
                MetaData = items.GetMetaData(),
            };
            return Ok(pagedCvs);
        }

        [HttpGet("Get/{cvId}")]
        public async Task<IActionResult> GetByIdAsync(int cvId)
        {
            return Ok(await _cvManagerService.GetByIdAsync(cvId));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(CvDTO cv)
        {
            try
            {
                var cvId =await _cvManagerService.CreateCvAsync(cv);

                return Created($"api/cvManager/get/{cvId}", cvId);
            }
            catch (Exception)
            {
                return Problem("Server error occured, please call the admin for details", statusCode: 500);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(CvDTO cv)
        {
            try
            {
                var cvId = await _cvManagerService.UpdateCvAsync(cv);
                return Ok(cvId);
            }
            catch (Exception)
            {
                return Problem("Server error occured, please call the admin for details", statusCode: 500);
            }
        }

        [HttpDelete("Delete/{cvId}")]
        public IActionResult Delete(int cvId)
        {
            try
            {
                _cvManagerService.DeleteCv(cvId);
                return NoContent();
            }
            catch (Exception)
            {
               return Problem("Server error occured, please call the admin for details", statusCode: 500);
            }
           
        }
    }
}