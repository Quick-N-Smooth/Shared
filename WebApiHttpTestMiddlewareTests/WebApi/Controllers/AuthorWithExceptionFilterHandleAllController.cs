using Microsoft.AspNetCore.Mvc;
using Omocom.BackOffice.Api.Filters;
using WebApi.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiExceptionFilter(handleUnknowException:true)]
    [ApiController]
    public class AuthorWithExceptionFilterHandleAllController : ControllerBase
    {
        ILogger<AuthorWithExceptionFilterController> logger;

        public AuthorWithExceptionFilterHandleAllController(ILogger<AuthorWithExceptionFilterController> logger)
            : base()
        {
            this.logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
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


        [HttpGet("ThrowUnknown/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ThrowUnknownException(int id)
        {

            var header = Request.Headers["culture"];

            if (string.IsNullOrEmpty(header))
            {
                throw new UnknownException("Unknown exception from Controller");
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

        [HttpGet("ThrowKnown/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ThrowKnownException(int id)
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
