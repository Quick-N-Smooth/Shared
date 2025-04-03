using MapperlyMapper._10_ExistingObjectAsTarget;

namespace MapperyMapperTests._10_ExistingObjectAsTarget
{
    [TestFixture]
    public class CarMapperTests
    {
        [Test]
        public void Map_HappyFlow()
        {
            var car = new Car 
            {
                ModelName = "Audi",
            };

            var dto = new CarDto(); 

            CarMapper.CarToCarDto(car, dto);

            Assert.That(dto.ModelName == "Audi",
                "Must be set");
        }
    }
}
