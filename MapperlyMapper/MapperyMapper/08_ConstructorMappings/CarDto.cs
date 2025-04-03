namespace MapperlyMapper._08_ConstructorMappings
{
    /// <summary>
    /// ModelName only has getter, and setter only available in ctor
    /// </summary>
    public class CarDto
    {

        public CarDto(string? model)
        {
            this.ModelName = model;
        }

        public string? ModelName { get; }
    }
}
