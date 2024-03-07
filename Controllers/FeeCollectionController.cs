using Membership_Managment.DAL.Interfaces;
using Membership_Managment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Membership_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeCollectionController : ControllerBase
    {
        private readonly IFeeCollectionRepository _feeRepo;

        public FeeCollectionController(IFeeCollectionRepository feeRepo)
        {
            _feeRepo = feeRepo;
        }

        [HttpPost()]
        public async Task<IActionResult> AddFeeAsync([FromForm]FeeCollection entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
               await _feeRepo.Add(entity);
                return Ok(entity);
            }
            catch (Exception e)
            {

                throw new ArgumentException("All fields are required",e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCollectedFee()
        {
            try
            {
                var a = await _feeRepo.GetAllAsync();
                return Ok(a);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var feeCollections = await _feeRepo.GetByMemberIdAsync(id);
                return Ok(feeCollections);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }



    }
}
