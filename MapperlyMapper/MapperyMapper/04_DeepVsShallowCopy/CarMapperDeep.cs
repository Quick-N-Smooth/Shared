using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._04_DeepVsShallowCopy
{
    [Mapper(UseDeepCloning = true)]
    public static partial class CarMapperDeep
    {
        public static partial CarDtoA1 CarToCarDtoA1(Car car);
    }
}
