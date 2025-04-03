using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._05_MapPropertyAttribute
{
    [Mapper]
    public static partial class CarMapper
    {

        [MapProperty(nameof(Car.Model), nameof(CarDto.ModelName))]
        public static partial CarDto CarToCarDto(Car car);

    }
}
