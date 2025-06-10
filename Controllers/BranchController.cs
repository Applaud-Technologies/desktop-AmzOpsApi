using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using desktop_AmzOpsApi.Models;

namespace desktop_AmzOpsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        private readonly ITestDataProvider _testData;
        public BranchController(ITestDataProvider testData)
        {
            _testData = testData;
        }

        [HttpGet]
        public ActionResult<List<Branch>> GetAll()
        {
            return Ok(_testData.GetBranches());
        }

        [HttpGet("{id}")]
        public ActionResult<Branch> GetById(int id)
        {
            var branch = _testData.GetBranches().Find(b => b.Id == id);
            if (branch == null) return NotFound();
            return Ok(branch);
        }
    }
}
