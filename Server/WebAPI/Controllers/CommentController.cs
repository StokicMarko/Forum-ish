using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : Controller
{
    
    [HttpGet("{id}")]
    public IActionResult GetSingle(int id)
    {
        return Ok();
    }
}