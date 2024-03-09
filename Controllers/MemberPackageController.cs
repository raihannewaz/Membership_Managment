using Membership_Managment.DAL.Interfaces;
using Membership_Managment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Membership_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberPackageController : ControllerBase
    {
        private readonly IMemberPackageRepository _mPackRepo;

        public MemberPackageController(IMemberPackageRepository mPackRepo)
        {
            _mPackRepo = mPackRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] MemberPackage entity)
        {
            if (ModelState.IsValid)
            {
                await _mPackRepo.Add(entity);
                return Ok(entity);
            }
            return BadRequest(ModelState);

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var a = await _mPackRepo.GetAllAsync();
            return Ok(a);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var a = await _mPackRepo.GetByIdAsync(id);
            return Ok(a);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _mPackRepo.GetByIdAsync(id);
            if (entity != null)
            {
                await _mPackRepo.Delete(entity);
                return Ok("MemberPackage Deleted!");
            }
            return BadRequest("Id Not Found");
        }
    }
}
