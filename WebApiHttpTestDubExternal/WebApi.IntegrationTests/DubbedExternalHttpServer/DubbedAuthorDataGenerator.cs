using WebApi.Entitites;

namespace WebApi.Tests.Dubbed
{
    public static class DubbedAuthorDataGenerator
    {

        private static readonly Dictionary<string, AuthorEntity> authors;

        static DubbedAuthorDataGenerator()
        {
            authors = new Dictionary<string, AuthorEntity>();
            authors.Add("Aut01",
            new AuthorEntity
            {
                AuId = "Aut01",
                AuFname = "Adam",
                AuLname = "Aaron",
                Address = "Herkules road 5",
                City = "Atlanta",
                HasContract = true,
                Zip = "A12569",
                State = "Alabama",
                Phone = "+15263561078"
            });
            authors.Add("Aut03",
            new AuthorEntity
            {
                AuId = "Aut03",
                AuFname = "Benjamin",
                AuLname = "Broom",
                Address = "Bing street 3",
                City = "Balmoral",
                HasContract = true,
                Zip = "B12569",
                State = null,
                Phone = "+21565289363"
            });
        }


        public static IEnumerable<AuthorEntity> AuthorEntities()
        {
            return authors.Values;
        }

        public static AuthorEntity? AuthorEntity(string id)
        {
            AuthorEntity ret;
            // Ignore return value
            authors.TryGetValue(id, out ret);
            return ret;
        }
    }
}
