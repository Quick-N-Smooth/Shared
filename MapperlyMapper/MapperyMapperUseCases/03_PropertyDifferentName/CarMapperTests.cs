using MapperlyMapper._03_PropertyDifferentName;

namespace MapperyMapperTests._03_PropertyDifferentName
{
    [TestFixture]
    public class CarMapperTests
    {
        [Test]
        public void Map_HappyFlow()
        {
            var car = new Car 
            {
                Manufacturer = new Manufacturer { Id = 12, Name = "Audi" }
            };

            var dto = CarMapper.CarToCarDto(car);

            Assert.That(dto.Producer is not null, "The producer must not be null");
            Assert.That(dto.Producer?.Id == car.Manufacturer?.Id);
            Assert.That(dto.Producer?.Name == car.Manufacturer?.Name);
        }
    }
}
