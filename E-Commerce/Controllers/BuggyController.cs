using E_Commerce.ResponseModule;
using Infrastrucure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("TestText")]
        [Authorize]
        public ActionResult<string> GetText()
        {
            return "Some Text";
        }
        [HttpGet("NotFound")]

        public ActionResult<string> GetNotFoundRequest()
        {
            var lablab = _context.Products.Find(1000);
            if (lablab == null)
                return NotFound(new ApiResponse(404));
            return Ok();
        }
        [HttpGet("BadRequest")]
        public ActionResult<string> GetBadRequest()
        {
       
           return BadRequest(new ApiResponse(400));
        }
    }
}
