using MapperlyMapper._02_EnumMappingByName;

namespace MapperyMapperTests._02_EnumMappingByName
{
    [TestFixture]
    public class CarMapperTests
    {
        [Test]
        public void Map_HappyFlow()
        {
            var mapper = new CarMapper();
            var car = new Car { Color = CarColor.Black };

            var dto = mapper.CarToCarDto(car);

            Assert.That(dto.Color == CarColorDto.Black, message: 
                "The color enum must match by name NOT by value");
        }
    }
}
