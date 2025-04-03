using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._10_ExistingObjectAsTarget
{
    [Mapper]
    public static partial class CarMapper
    {
        public static partial void CarToCarDto(Car car, CarDto dto);
    }
}
