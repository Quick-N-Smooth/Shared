
using WebApi.Entitites;
using WebApi.HttpClients;

namespace WebApi.Tests.Dubbed
{
    public class DubbedAuthorClient : IAuthorClient
    {

        public async Task<AuthorEntity> GetAuthor(string id)
        {
            return await Task.FromResult(DubbedAuthorDataGenerator.AuthorEntity(id));
        }

        public async Task<IEnumerable<AuthorEntity>> GetAuthors()
        {
            return await Task.FromResult(DubbedAuthorDataGenerator.AuthorEntities());
        }
    }
}
