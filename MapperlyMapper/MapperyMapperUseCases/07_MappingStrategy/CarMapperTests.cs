using MapperlyMapper._07_MappingStrategy;

namespace MapperyMapperTests._07_MappingStrategy
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

            var dtoA1 = CarMapperCaseSensitive.CarToCarDto(car);

            Assert.That(dtoA1.modelName is null,
                "Must be the null as it is mapped by case sensitive.");

            var dtoA2 = CarMapperCaseInsensitive.CarToCarDto(car);

            Assert.That(dtoA2.modelName is not null,
                "Must be NOT null as it is mapped by case insensitive.");

        }
    }
}
