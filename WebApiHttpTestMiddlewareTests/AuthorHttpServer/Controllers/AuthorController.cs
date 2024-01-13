using AuthorsHttpServer.Data;
using AuthorsHttpServer.Entitites;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        ILogger<AuthorsController> logger;
        private readonly IConfiguration configuration;

        public AuthorsController(ILogger<AuthorsController> logger, IConfiguration configuration)
            : base()
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        // GET: api/<AuthorController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AuthorEntity>))]
        public async Task<IActionResult> Get()
        {
            var authors = 
                await Task.FromResult(AuthorDataGenerator.AuthorEntities());
            return Ok(authors);
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorEntity))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var author =
                await Task.FromResult(AuthorDataGenerator.AuthorEntity(id));

            if (author == null)
                return NotFound();

            return Ok(author);
        }
    }
}
