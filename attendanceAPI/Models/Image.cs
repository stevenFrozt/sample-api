
namespace attendanceAPI.Models
{
    public class Image
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public string Path { get; set; } = "";
        public string Size { get; set; } = "";
        public string Type { get; set; } = "";
        public string? Width { get; set; } = "";
        public string? Height { get; set; } = "";


    }
}