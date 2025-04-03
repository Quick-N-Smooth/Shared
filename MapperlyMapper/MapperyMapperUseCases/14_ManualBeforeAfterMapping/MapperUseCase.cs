using MapperlyMapper._14_ManualBeforeAfterMapping;

namespace MapperyMapperTests._14_ManualBeforeAfterMapping
{
    [TestFixture]
    public class MapperUseCase
    {
        [Test]
        public void Map_HappyFlow()
        {
            Owner Bob = new Owner() { FirstName = "Bob", MiddleName = "the", LastName = "Builder" };

            var mapper = new OwnerMapper();

            var dto = mapper.OwnerToOwnerDto(Bob);

            // chech for different property in nested dto ()
            Assert.That(dto.FullName == "Bob the Builder");
        }
    }
}
