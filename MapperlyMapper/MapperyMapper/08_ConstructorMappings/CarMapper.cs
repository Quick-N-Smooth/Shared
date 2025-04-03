using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._08_ConstructorMappings
{
    [Mapper]
    public static partial class CarMapper
    {
        [MapProperty(nameof(Car.ModelName), "model")]
        public static partial CarDto CarToCarDto(Car car);


        public static partial CarDtoA2 CarToCarDtoA2(Car car);
    }
}
