using System.Text.Json.Serialization;

namespace WebApi.ViewModels
{
    public class AuthorModel
    {
        [JsonPropertyName("fullName")] 
        public string FullName { get; set; } = null!;

        [JsonPropertyName("phone")]
        public string Phone { get; set; } = null!;

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("zipCode")]
        public string? ZipCode { get; set; }
    }
}
