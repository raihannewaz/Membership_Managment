using Membership_Managment.DAL.Interfaces;
using Membership_Managment.DAL.Repositories;
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
        public async Task<IActionResult> Add([FromForm]Payment entity)
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

        [HttpGet("nextPaymentDates")]
        public async Task<IActionResult> GetNextPaymentDates()
        {
            var nextPaymentDates = await _payRepo.GetNextPaymentDates();
            return Ok(nextPaymentDates);
        }

        [HttpGet("next-payment/{memberId}")]
        public async Task<ActionResult<DateTime?>> GetNextPaymentDate(int memberId)
        {
            try
            {
                var nextPaymentDate = await _payRepo.GetNextPaymentDateByMemberId(memberId);

                if (nextPaymentDate != null)
                {
                    return Ok(nextPaymentDate);
                }
                else
                {
                    return NotFound("No next payment date found for the member.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
