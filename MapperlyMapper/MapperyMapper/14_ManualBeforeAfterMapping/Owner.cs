namespace MapperlyMapper._14_ManualBeforeAfterMapping
{
    public class Owner
    {
        public required string FirstName { get; init; }

        public string? MiddleName { get; init; }

        public required string LastName { get; init; }

        public DateTime BirthDate { get; set; }

    }
}
