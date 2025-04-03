using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._13_ManualTypeMapping
{
    [Mapper]
    public partial class OwnerMapper
    {

        [MapProperty(nameof(Owner.BirthDate), nameof(OwnerDto.BirthDateString))] // Map property with a different name in the target type
        public partial OwnerDto OwnerToOwnerDto(Owner model);

        private DateOnly BirthDate(DateTime birthtime) => new DateOnly( birthtime.Year, birthtime.Month, birthtime.Day);

        private string BirthDateString(DateTime birthtime) => birthtime.ToString("yyyy-MM-dd");


    }
}
