using Microsoft.AspNetCore.Mvc;
using Omocom.BackOffice.Api.Filters;
using WebApi.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilter]
    public class AuthorWithExceptionFilterController : ControllerBase
    {
        ILogger<AuthorWithExceptionFilterController> logger;

        public AuthorWithExceptionFilterController(ILogger<AuthorWithExceptionFilterController> logger)
            : base()
        {
            this.logger = logger;
        }


        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {

            var header = Request.Headers["culture"];

            if (string.IsNullOrEmpty(header))
            {
                throw new ArgumentNullException("Argument null exception from Controller");
            }

            var model = new AuthorModel
            {
                FullName = $"Adam Ant",
                Address = "Ground street 1",
                City = "AntCity",
                Phone = "123456789",
                ZipCode = "1234"
            };

            return Ok(model);
        }
    }
}
