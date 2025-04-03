using MapperlyMapper._04_DeepVsShallowCopy;

namespace MapperyMapperTests._04_DeepVsShallowCopy
{
    [TestFixture]
    public class CarMapperTests
    {
        [Test]
        public void Map_ShallowVsDeepCloningAlternatives()
        {
            var car = new Car 
            {
                Producer = new Producer { Id = 12, Name = "Audi" }
            };

            var dtoA1 = CarMapper.CarToCarDtoA1(car);

            Assert.That(ReferenceEquals(dtoA1.Producer, car.Producer) , 
                "The producer is the same object type type, it is NOT CLONED");

            var dtoA2 = CarMapper.CarToCarDtoA2(car);

            Assert.That(!ReferenceEquals(dtoA2.Producer, car.Producer),
                "The producer is the dto object type in the target, it is CLONED");

            var dtoA3 = CarMapperDeep.CarToCarDtoA1(car);

            Assert.That(!ReferenceEquals(dtoA3.Producer, car.Producer),
                "The producer is the same type, but Mapper copy behaviour is DeepCopy, it is CLONED");

        }
    }
}
