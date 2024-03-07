using Membership_Managment.DAL.Interfaces;
using Membership_Managment.DAL.Repositories;
using Membership_Managment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Document = Membership_Managment.Models.Document;

namespace Membership_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {

        private IDocumentRepository _documentRepository;
        

        public DocumentController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }



        [HttpPost("{memberId}")]
        public async Task<IActionResult> AddDoc(int memberId, [FromForm]Document document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _documentRepository.AddNew(memberId, document);
                return Ok("Photos uploaded successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, "An error occurred while uploading photos.");
            }
        }

        [HttpPut("{memberId}")]
        public async Task<IActionResult> UpdateDoc(int memberId, [FromForm]Document document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _documentRepository.UpdateDoc(memberId, document);
                return Ok("Updated");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while uploading photos.");
            }
        }


        [HttpGet("/getNid/{memberid}")]
        public IActionResult GetNidOfMember(int memberid)
        {
            var a = _documentRepository.GetNidByMember(memberid);
            return Ok(a);
           
        }


        [HttpGet("/getUtility/{memberid}")]
        public IActionResult GetUtilityOfMember(int memberid)
        {
            var a = _documentRepository.GetUtilityByMember(memberid);
            return Ok(a);
        }

    }
}
