using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._06_IgnorePropertyAttribute
{
    [Mapper]
    public static partial class CarMapper
    {

        [MapperIgnoreTarget(nameof(CarDto.MakeId))]
        [MapperIgnoreSource(nameof(Car.Id))]
        public static partial CarDto CarToCarDto(Car car);

    }
}
