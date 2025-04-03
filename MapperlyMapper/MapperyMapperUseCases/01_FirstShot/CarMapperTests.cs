using MapperlyMapper._01_FirstShot;

namespace MapperyMapperTests._01_FirstShot
{
    [TestFixture]
    public class CarMapperTests
    {
        [Test]
        public void Map_CarToCarDto_HappyFlow()
        {
            var mapper = new CarMapper();
            var car = new Car { NumberOfSeats = 10 };

            var dto = mapper.CarToCarDto(car);

            Assert.That(dto.NumberOfSeats == 10);
        }

        [Test]
        public void Map_CarDtoToCar_HappyFlow()
        {
            var mapper = new CarMapper();
            var carDto = new CarDto { NumberOfSeats = 10 };

            var car = mapper.CarDtoToCar(carDto);

            Assert.That(car.NumberOfSeats == 10);
        }
    }
}
