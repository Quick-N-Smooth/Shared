using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._07_MappingStrategy
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseSensitive)]
    public static partial class CarMapperCaseSensitive
    {
        public static partial CarDto CarToCarDto(Car car);
    }
}
