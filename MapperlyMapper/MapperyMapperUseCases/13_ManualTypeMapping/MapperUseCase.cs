using MapperlyMapper._13_ManualTypeMapping;

namespace MapperyMapperTests._13_ManualTypeMapping
{
    [TestFixture]
    public class MapperUseCase
    {
        [Test]
        public void Map_MapBirthDate_HappyFlow()
        {
            Owner Bob = new Owner() { BirthDate = new DateTime(1968, 05, 10, 14, 30, 00 ) };

            var mapper = new OwnerMapper();

            var dto = mapper.OwnerToOwnerDto(Bob);

            // chech for different property in nested dto ()
            Assert.That(dto.BirthDate.Year == 1968);
            Assert.That(dto.BirthDate.Month == 5);
            Assert.That(dto.BirthDate.Day == 10);
        }

        [Test]
        public void Map_MapBirthDateString_HappyFlow()
        {
            Owner Bob = new Owner() { BirthDate = new DateTime(1968, 05, 10, 14, 30, 00) };

            var mapper = new OwnerMapper();

            var dto = mapper.OwnerToOwnerDto(Bob);

            // chech for different property in nested dto ()
            Assert.That(dto.BirthDateString == "1968-05-10");
        }
    }
}
