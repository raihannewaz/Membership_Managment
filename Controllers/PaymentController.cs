using Membership_Managment.DAL.Interfaces;
using Membership_Managment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Membership_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _payRepo;

        public PaymentController(IPaymentRepository payRepo)
        {
            _payRepo = payRepo;
        }


        [HttpPost]
        public async Task<IActionResult> Add(Payment entity)
        {
            if (ModelState.IsValid)
            {
                await _payRepo.Add(entity);
                return Ok(entity);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetCollectedFee()
        {
            try
            {
                var a = await _payRepo.GetAllAsync();
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
                var feeCollections = await _payRepo.GetByMemberIdAsync(id);
                return Ok(feeCollections);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
