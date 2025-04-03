using MapperlyMapper._06_IgnorePropertyAttribute;

namespace MapperyMapperTests._06_IgnorePropertyAttribute
{
    [TestFixture]
    public class CarMapperTests
    {
        [Test]
        public void Map_MapProperty()
        {
            var car = new Car 
            {
                Model = "Audi",
                ModelId = 11,
                Id = 1,
                MakeId = "Ignored do not map"
            };

            var dto = CarMapper.CarToCarDto(car);

            Assert.That(dto.Id is null,
                "Must be the null as it is ignored.");
            Assert.That(dto.MakeId is null,
                "Must be the null as it is ignored.");

        }
    }
}
