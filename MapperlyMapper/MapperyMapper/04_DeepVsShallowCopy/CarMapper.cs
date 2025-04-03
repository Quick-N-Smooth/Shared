using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._04_DeepVsShallowCopy
{
    [Mapper]
    public static partial class CarMapper
    {
        public static partial CarDtoA1 CarToCarDtoA1(Car car);

        public static partial CarDtoA2 CarToCarDtoA2(Car car);

    }
}
