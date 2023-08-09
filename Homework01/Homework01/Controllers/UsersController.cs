using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetAllUsers()
        {
            return Ok(StaticDb.Users);
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetUser(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("The id can not be negative");
                }
                if (id >= StaticDb.Users.Count)
                {
                    return NotFound($"The id you chose can not be found");
                }
                return Ok(StaticDb.Users[id]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured");
            }
        }
    }
}
