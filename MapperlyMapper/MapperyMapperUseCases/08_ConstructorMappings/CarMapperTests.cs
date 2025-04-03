using MapperlyMapper._08_ConstructorMappings;

namespace MapperyMapperTests._08_ConstructorMappings
{
    [TestFixture]
    public class CarMapperTests
    {
        [Test]
        public void Map_MappingStrategy()
        {
            var car = new Car 
            {
                ModelName = "Audi",
            };

            var dto = CarMapper.CarToCarDto(car);

            Assert.That(dto.ModelName == "Audi",
                "Must be set at constructor");

            var dtoA2 = CarMapper.CarToCarDto(car);

            Assert.That(dtoA2.ModelName == "Audi",
                "Must be set at constructor");

        }
    }
}
