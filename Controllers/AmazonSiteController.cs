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
    public class AmazonSiteController : ControllerBase
    {
        private readonly AmazonSiteService _service;
        private readonly ITestDataProvider _testData;

        public AmazonSiteController(AmazonSiteService service, ITestDataProvider testData)
        {
            _service = service;
            _testData = testData;
        }

        [HttpGet]
        public ActionResult<List<AmazonSite>> GetAll()
        {
            var sites = _testData.GetSites();
            return Ok(sites);
        }

        [HttpGet("{id}")]
        public ActionResult<AmazonSite> GetById(int id)
        {
            var site = _testData.GetSites().Find(s => s.Id == id);
            if (site == null)
                return NotFound();
            return Ok(site);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AmazonSite site)
        {
            try
            {
                _service.CreateSite(site);
                return CreatedAtAction(nameof(GetById), new { id = site.Id }, site);
            }
            catch (NotImplementedException)
            {
                return StatusCode(501, "CreateSite not implemented");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AmazonSite site)
        {
            if (id != site.Id)
                return BadRequest();
            try
            {
                _service.UpdateSite(site);
                return NoContent();
            }
            catch (NotImplementedException)
            {
                return StatusCode(501, "UpdateSite not implemented");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.DeleteSite(id);
                return NoContent();
            }
            catch (NotImplementedException)
            {
                return StatusCode(501, "DeleteSite not implemented");
            }
        }

        [HttpPost("save")]
        public async Task<ActionResult<SaveResult<AmazonSite>>> SaveChanges([FromBody] IEnumerable<AmazonSite> sites)
        {
            var result = await _service.SaveChangesAsync(sites);
            return Ok(result);
        }
    }
}
