namespace Span.Culturio.Api.Models
{
    public class CreatePackageDto
    {
        public string Name { get; set; }
        public int[] CultureObjectIds { get; set; }
        public int ValidDays { get; set; }
    }
}
