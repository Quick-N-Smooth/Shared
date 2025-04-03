using MapperlyMapper._12_NestedPropertyDifferentName;

namespace MapperyMapperTests._12_NestedPropertyDifferentName
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
                Brand = "Audi",
                Manufacturer = new Manufacturer { Name = "VW" }
            };

            var carVW = new Car
            {
                Brand = "Peugeot",
                Manufacturer = new Manufacturer { Name = "PSA Group" }
            };

            Bob.Cars = new List<Car> { carAudi, carVW }.ToArray();

            var dto = OwnerMapper.OwnerToOwnerDto(Bob);

            Assert.That(dto.Cars.Count() == 2,
                "Cars must be set");

            // chech for different type of dto
            Assert.That(dto.Cars[0].Producer.Name == "VW");
            Assert.That(dto.Cars[1].Producer.Name == "PSA Group");

            // chech for different property in nested dto ()
            Assert.That(dto.Cars[0].Make == "Audi");
            Assert.That(dto.Cars[1].Make == "Peugeot");

        }
    }
}
