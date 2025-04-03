using Riok.Mapperly.Abstractions;


namespace MapperlyMapper._01_FirstShot
{
    [Mapper]
    public partial class CarMapper
    {
        public partial CarDto CarToCarDto(Car car);

        public partial Car CarDtoToCar(CarDto car);
    }
}
