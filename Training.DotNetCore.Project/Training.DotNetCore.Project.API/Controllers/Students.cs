using Microsoft.AspNetCore.Mvc;

namespace Training.DotNetCore.Project.API.Controllers
{
    //https://lcoalhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class Students : ControllerBase
    {
        private static readonly string[] studentsName = ["Faisal", "Karthik", "Barvin"];

        [HttpGet]
        [Route("GetStudents")]
        public IActionResult GetAllStudents()
        {
            return Ok(studentsName);
        }
        [HttpGet]
        [Route("GetStudent")]
        public IActionResult GetStudent()
        {
            //string[] studentsName = new string[] { "Faisal", "Karthik", "Barvin" };
            return Ok(studentsName.Where(x => x.StartsWith("Faisal")));
        }
    }
}
