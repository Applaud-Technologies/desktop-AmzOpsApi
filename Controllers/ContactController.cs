using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using desktop_AmzOpsApi.Models;

namespace desktop_AmzOpsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ITestDataProvider _testData;
        public ContactController(ITestDataProvider testData)
        {
            _testData = testData;
        }

        [HttpGet]
        public ActionResult<List<Contact>> GetAll()
        {
            return Ok(_testData.GetContacts());
        }

        [HttpGet("{id}")]
        public ActionResult<Contact> GetById(int id)
        {
            var contact = _testData.GetContacts().Find(c => c.Id == id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpGet("ByBranch/{branchId}")]
        public ActionResult<List<Contact>> GetByBranch(int branchId)
        {
            var contacts = _testData.GetContacts().FindAll(c => c.BranchId == branchId);
            return Ok(contacts);
        }
    }
}
