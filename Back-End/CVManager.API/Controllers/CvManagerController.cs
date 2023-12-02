using CVManager.Core.DTOs;
using CVManager.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult GetAll(int pageNumber, int pageSize)
        {
            var items = _cvManagerService.GetCvsList(pageNumber, pageSize);

            var pagedCvs = new PagedList<CvDTO>
            {
                Items = items,
                MetaData = items.GetMetaData(),
            };
            return Ok(pagedCvs);
        }

        [HttpGet("Get/{cvId}")]
        public IActionResult GetById(int cvId)
        {
            return Ok(_cvManagerService.GetById(cvId));
        }

        [HttpPost("Create")]
        public IActionResult Create(CvDTO cv)
        {
            try
            {
                var cvId = _cvManagerService.CreateCv(cv);

                return Created($"api/cvManager/get/{cvId}", cvId);
            }
            catch (Exception)
            {
                return Problem("Server error occured, please call the admin for details", statusCode: 500);
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(CvDTO cv)
        {
            try
            {
                var cvId = _cvManagerService.UpdateCv(cv);
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