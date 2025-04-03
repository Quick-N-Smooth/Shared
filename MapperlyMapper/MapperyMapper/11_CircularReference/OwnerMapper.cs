using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._11_CircularReference
{
    //[Mapper]
    [Mapper(UseReferenceHandling = true)]
    public partial class OwnerMapper
    {
        public partial OwnerDto OwnerToOwnerDto(Owner car);
    }
}
