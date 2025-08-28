using Microsoft.AspNetCore.Mvc;

namespace YATT.Controllers;

[ApiController]
[Route("[controller]")]
public class DummyController : ControllerBase
{
    [HttpGet]
    [Route("hello")]
    public ActionResult<string> GetHello()
    {
        return Ok("Hello World");
    }
}
