using MapperlyMapper._09_ObjectFactory;

namespace MapperyMapperTests._09_ObjectFactory
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
                Seats = 10
            };

            var dto = new CarMapper().CarToCarDto(car);

            Assert.That(dto.ModelName == "Audi created in object factory method",
                "Must be set by a factory method");

            Assert.That(dto.ModelName != "Audi created in constructor",
                "Constuctor must not be used.");

            Assert.That(dto.Seats == 10, 
                "This is a normal setter which is not set in the ctor");

        }
    }
}
