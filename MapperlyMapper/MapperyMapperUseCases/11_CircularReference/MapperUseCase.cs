using MapperlyMapper._11_CircularReference;

namespace MapperyMapperTests._11_CircularReference
{
    [TestFixture]
    public class MapperUseCase
    {
        [Test]
        public void Map_HappyFlow()
        {
            Owner Bob = new Owner();

            var carAudi = new Car 
            {
                ModelName = "Audi",
                Owner = Bob
            };

            var carVW = new Car
            {
                ModelName = "VW",
                Owner = Bob
            };

            Bob.Cars = new List<Car> { carAudi, carVW }.ToArray();

            var mapper = new OwnerMapper();

            var dto = mapper.OwnerToOwnerDto(Bob);

            Assert.That(dto.Cars.Count() == 2,
                "Cars must be set");

            Assert.That(dto.Cars[0].ModelName == "Audi", "One car should be Audi");
            Assert.That(dto.Cars[1].ModelName == "VW", "The other should be VW");

            Assert.That(ReferenceEquals(dto.Cars[0].Owner, dto), "Cars owner should be referenced");
            Assert.That(ReferenceEquals(dto.Cars[1].Owner, dto), "Cars owner should be referenced");

        }
    }
}
