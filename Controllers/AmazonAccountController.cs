using Microsoft.AspNetCore.Mvc;
using desktop_AmzOpsApi.Models;
using desktop_AmzOpsApi.DAL;
using System.Collections.Generic;
using desktop_AmzOpsApi.BLL;
using System;

namespace desktop_AmzOpsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmazonAccountController : ControllerBase
    {
        private readonly AmazonAccountService _service;
        private readonly ITestDataProvider _testData;

        public AmazonAccountController(AmazonAccountService service, ITestDataProvider testData)
        {
            _service = service;
            _testData = testData;
        }

        [HttpGet]
        public ActionResult<List<AmazonAccount>> GetAll()
        {
            var accounts = _testData.GetAccounts();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public ActionResult<AmazonAccount> GetById(string id)
        {
            var account = _testData.GetAccounts().Find(a => a.AccountNumber == id);
            if (account == null)
                return NotFound();
            return Ok(account);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AmazonAccount account)
        {
            try
            {
                _service.CreateAccount(account);
                return CreatedAtAction(nameof(GetById), new { id = account.AccountNumber }, account);
            }
            catch (NotImplementedException)
            {
                return StatusCode(501, "CreateAccount not implemented");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] AmazonAccount account)
        {
            if (id != account.AccountNumber)
                return BadRequest();
            try
            {
                _service.UpdateAccount(account);
                return NoContent();
            }
            catch (NotImplementedException)
            {
                return StatusCode(501, "UpdateAccount not implemented");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _service.DeleteAccount(id);
                return NoContent();
            }
            catch (NotImplementedException)
            {
                return StatusCode(501, "DeleteAccount not implemented");
            }
        }
    }
}
