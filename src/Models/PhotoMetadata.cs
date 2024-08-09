using MetadataExtractor;
using System.Globalization;
using System.Text.Json.Serialization;
using Directory = MetadataExtractor.Directory;

namespace PhotoCarousel.Models;

public class PhotoMetadata(string filename)
{
    private static readonly CultureInfo culture = new CultureInfo("en-US");

    [JsonPropertyName("filename")]
    public string Filename { get; } = filename.Replace("wwwroot", string.Empty);

    [JsonPropertyName("sortstamp")]
    public DateTime? DateTaken { get; } = GetDateTaken(filename);

    [JsonPropertyName("datestamp")]
    public string? Datestamp => DateTaken?.ToString("Y", culture);

    public static DateTime? GetDateTaken(string file)
    {
        string? timestamp = ImageMetadataReader.ReadMetadata(file)
            .FirstOrDefault(x => x.Name == "Exif SubIFD")?.Tags
            .FirstOrDefault(x => x.Name == "Date/Time Original")?.Description;

        if (timestamp is null)
        {
            return null;
        }

        return DateTime.ParseExact(timestamp, "yyyy:MM:dd HH:mm:ss", culture);
    }

    public static IEnumerable<Directory> GetAllMetadata(string file)
        => ImageMetadataReader.ReadMetadata(file);
}

