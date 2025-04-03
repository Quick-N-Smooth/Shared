using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._12_NestedPropertyDifferentName
{
    [Mapper]
    public static partial class OwnerMapper
    {

        public static partial OwnerDto OwnerToOwnerDto(Owner model);

        // handles an nested property
        [MapProperty(nameof(Car.Manufacturer), nameof(CarDto.Producer))]
        [MapProperty(nameof(Car.Brand), nameof(CarDto.Make))]
        public static partial CarDto CarToCarDto(Car model);


    }
}
