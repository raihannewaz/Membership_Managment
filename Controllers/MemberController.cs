using Membership_Managment.DAL.Interfaces;
using Membership_Managment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Membership_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private IMemberRepository _memberRepository;

        public MemberController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var members = await _memberRepository.GetAllAsync();
            return Ok(members);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            return Ok(member);
        }

        [HttpPost]
        public async Task<IActionResult> AddMember([FromForm]Member member)
        {
            if (ModelState.IsValid)
            {
                var addmember = await _memberRepository.Add(member);
                return Ok(addmember);
            }

            return BadRequest();
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromForm]Member member)
        {
            var existingEntity = await _memberRepository.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return NotFound();
            }

            var updatedEntity = await _memberRepository.Update(id, member);
            return Ok(updatedEntity);

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var memberRemove = await _memberRepository.GetByIdAsync(id);
            if (memberRemove != null)
            {
                await _memberRepository.Delete(memberRemove);
                return Ok();
            }
            return BadRequest();
        }


    }
}
