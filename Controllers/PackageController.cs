using Membership_Managment.DAL.Interfaces;
using Membership_Managment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Membership_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageRepository _packRepo;

        public PackageController(IPackageRepository packRepo)
        {
            _packRepo = packRepo;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pack = await _packRepo.GetAllAsync();
            return Ok(pack);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pack = await _packRepo.GetByIdAsync(id);
            return Ok(pack);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Package pack)
        {
            if (ModelState.IsValid)
            {
                var entity = await _packRepo.Add(pack);
                return Ok(entity);
            }

            return BadRequest();
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] Package pack)
        {
            var existingEntity = await _packRepo.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return NotFound();
            }

            var updatedEntity = await _packRepo.Update(id, pack);
            return Ok(updatedEntity);

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entityRemove = await _packRepo.GetByIdAsync(id);
            if (entityRemove != null)
            {
                await _packRepo.Delete(entityRemove);
                return Ok();
            }
            return BadRequest();
        }
    }
}
