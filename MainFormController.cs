using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace desktopAmzOpsApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainFormController : ControllerBase
    {
        private readonly string _connectionString;
        public MainFormController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
