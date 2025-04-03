using Riok.Mapperly.Abstractions;

namespace MapperlyMapper._09_ObjectFactory
{
    [Mapper]
    public partial class CarMapper
    {
        [ObjectFactory]
        private CarDto CreateCarDto(Car car)
            => CarDto.CreateFromCustomMethod(car);

        public partial CarDto CarToCarDto(Car car);
    }
}
