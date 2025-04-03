using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._02_EnumMappingByName
{
    // NOTE THAT IT IS ALSO RECOMMENDED TO IGNORE CASE

    [Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName, EnumMappingIgnoreCase = true)]
    public partial class CarMapper
    {
        public partial CarDto CarToCarDto(Car car);
    }
}
