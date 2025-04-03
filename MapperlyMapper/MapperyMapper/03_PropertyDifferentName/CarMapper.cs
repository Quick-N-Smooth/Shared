using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._03_PropertyDifferentName
{
    [Mapper]
    public static partial class CarMapper
    {
        [MapProperty(nameof(Car.Manufacturer), nameof(CarDto.Producer))] // Map property with a different name in the target type
        public static partial CarDto CarToCarDto(Car car);
    }
}
