using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebApi.HttpClients;
using WebApi.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        ILogger<AuthorsController> logger;
        private readonly IConfiguration configuration;
        private readonly IAuthorClient client;

        public AuthorsController(ILogger<AuthorsController> logger, IConfiguration configuration, IAuthorClient client) 
            : base()
        {
            this.logger = logger;
            this.configuration = configuration;
            this.client = client;
        }

        // GET: api/<AuthorController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AuthorViewModel>))]
        public async Task<IActionResult> Get()
        {

            var authors = await client.GetAuthors()
                .ContinueWith(t => t.Result.Select
                (i => new AuthorViewModel
                {
                    FullName = $"{i.AuFname} {i.AuLname}",
                    Address = i.Address,
                    City = i.City,
                    Phone = i.Phone,
                    ZipCode = i.Zip
                }));
            return Ok(authors);

        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var author = await client.GetAuthor(id);

            if (author == null)
                return NotFound();

            return Ok(new AuthorViewModel
            {
                FullName = $"{author.AuFname} {author.AuLname}",
                Address = author.Address,
                City = author.City,
                Phone = author.Phone,
                ZipCode = author.Zip
            });
        }
    }
}
