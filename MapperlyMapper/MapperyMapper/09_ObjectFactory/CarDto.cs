namespace MapperlyMapper._09_ObjectFactory
{
    /// <summary>
    /// ModelName only has getter, and setter only available in a private ctor
    /// CreateFromCustomMethod factory method is used
    /// </summary>
    public class CarDto
    {
        public static CarDto CreateFromCustomMethod(Car car)
        {
            var o = new CarDto();
            o.ModelName += $"{car.ModelName} created in object factory method";
            return o;
        }

        private CarDto()
        {          
        }

        /// <summary>
        /// This constrructor SHOULD NOT BE USED because of the CreateFromCustomMethod
        /// Object factory
        /// </summary>
        /// <param name="model"></param>
        public CarDto(string? model)
        {
            this.ModelName = model + " created in constructor";
        }

        public string? ModelName { get; private set; }

        public int? Seats { get; set; }
    }
}
