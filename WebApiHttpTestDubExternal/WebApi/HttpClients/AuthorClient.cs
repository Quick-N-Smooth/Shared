
using WebApi.Entitites;
using WebApi.HttpClients;

namespace WebApi.HttpClients
{
    public class AuthorClient : IAuthorClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthorClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<AuthorEntity> GetAuthor(string id)
        {
            var client = _httpClientFactory.CreateClient(Clients.CrmApiClient);
            return await client.GetFromJsonAsync<AuthorEntity>($"api/authors/{id}");
        }

        public async Task<IEnumerable<AuthorEntity>> GetAuthors()
        {
            var client = _httpClientFactory.CreateClient(Clients.CrmApiClient);
            return await client.GetFromJsonAsync<IEnumerable<AuthorEntity>>($"api/authors");
        }
    }

    public interface IAuthorClient
    {
        Task<IEnumerable<AuthorEntity>> GetAuthors();
        Task<AuthorEntity> GetAuthor(string id)
            ;
    }
}
