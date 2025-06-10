using Microsoft.AspNetCore.Mvc;
using desktop_AmzOpsApi.Models;
using desktop_AmzOpsApi.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;
using desktop_AmzOpsApi.BLL;
using System;
using AmazonOperations.Common;

namespace desktop_AmzOpsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmazonTeamMemberController : ControllerBase
    {
        private readonly AmazonTeamMemberService _service;
        private readonly ITestDataProvider _testData;

        public AmazonTeamMemberController(AmazonTeamMemberService service, ITestDataProvider testData)
        {
            _service = service;
            _testData = testData;
        }

        [HttpGet]
        public ActionResult<List<AmazonTeamMember>> GetAll()
        {
            var teamMembers = _testData.GetTeamMembers();
            return Ok(teamMembers);
        }

        [HttpGet("{id}")]
        public ActionResult<AmazonTeamMember> GetById(string id)
        {
            var teamMember = _testData.GetTeamMembers().Find(t => t.AdpEmployeeId == id);
            if (teamMember == null)
                return NotFound();
            return Ok(teamMember);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AmazonTeamMember teamMember)
        {
            try
            {
                _service.CreateTeamMember(teamMember);
                return CreatedAtAction(nameof(GetById), new { id = teamMember.AdpEmployeeId }, teamMember);
            }
            catch (NotImplementedException)
            {
                return StatusCode(501, "CreateTeamMember not implemented");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] AmazonTeamMember teamMember)
        {
            if (id != teamMember.AdpEmployeeId)
                return BadRequest();
            try
            {
                _service.UpdateTeamMember(teamMember);
                return NoContent();
            }
            catch (NotImplementedException)
            {
                return StatusCode(501, "UpdateTeamMember not implemented");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _service.DeleteTeamMember(id);
                return NoContent();
            }
            catch (NotImplementedException)
            {
                return StatusCode(501, "DeleteTeamMember not implemented");
            }
        }

        [HttpPost("save")]
        public async Task<ActionResult<SaveResult<AmazonTeamMember>>> SaveChanges([FromBody] IEnumerable<AmazonTeamMember> teamMembers)
        {
            var result = await _service.SaveChangesAsync(teamMembers);
            return Ok(result);
        }
    }
}
