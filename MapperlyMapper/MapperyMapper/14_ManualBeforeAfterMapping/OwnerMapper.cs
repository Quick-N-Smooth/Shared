using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._14_ManualBeforeAfterMapping
{
    [Mapper]
    public partial class OwnerMapper
    {
        

        // NESTING THE MAPPER METHOD
        public OwnerDto OwnerToOwnerDto(Owner model)
        {

            // BEFORE MAPPING

            var dto = OwnerToOwnerDtoIntern(model);

            // AFTER MAPPING
            dto.FullName = $"{model.FirstName} {model.MiddleName ?? string.Empty} {model.LastName}";

            return dto;

        }

        // THE REAL MAPPING METHOD
        [MapperIgnoreSource(nameof(Owner.FirstName))]
        [MapperIgnoreSource(nameof(Owner.LastName))]
        private partial OwnerDto OwnerToOwnerDtoIntern(Owner model);

    }
}
