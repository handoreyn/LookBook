using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/api/member")]
public class MemberController : ControllerBase
{
    [Route("hi")]
    [HttpGet]
    public IActionResult Hi() => Ok("shit");
}