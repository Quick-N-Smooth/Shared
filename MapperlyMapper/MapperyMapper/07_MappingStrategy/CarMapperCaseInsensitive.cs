using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._07_MappingStrategy
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    public static partial class CarMapperCaseInsensitive
    {
        public static partial CarDto CarToCarDto(Car car);
    }
}
