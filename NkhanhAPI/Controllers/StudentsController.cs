using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NkhanhAPI.Controllers
{
    // https://localhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: https://localhost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "nam", "khánh", "minh", "khay", };
            return Ok(studentNames);

        }
    }
}
