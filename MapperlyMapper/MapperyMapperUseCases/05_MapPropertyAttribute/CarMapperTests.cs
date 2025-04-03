using MapperlyMapper._05_MapPropertyAttribute;

namespace MapperyMapperTests._05_MapPropertyAttribute
{
    [TestFixture]
    public class CarMapperTests
    {
        [Test]
        public void Map_MapProperty()
        {
            var car = new Car 
            {
                Model = "Audi"
            };

            var dto = CarMapper.CarToCarDto(car);

            Assert.That(dto.ModelName == car.Model,
                "Must be the same value");

        }
    }
}
