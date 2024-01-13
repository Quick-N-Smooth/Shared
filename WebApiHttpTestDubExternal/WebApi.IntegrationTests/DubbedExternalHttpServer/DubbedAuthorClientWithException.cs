
using WebApi.Entitites;
using WebApi.HttpClients;

namespace WebApi.Tests.Dubbed
{
    public class DubbedAuthorClientWithException : IAuthorClient
    {

        public async Task<AuthorEntity> GetAuthor(string id)
        {
            throw new Exception("Test exception");
        }

        public async Task<IEnumerable<AuthorEntity>> GetAuthors()
        {
            throw new Exception("Test exception");
        }
    }
}
