using Microsoft.Extensions.Caching.Memory;
using PhotoCarousel.Interfaces;
using PhotoCarousel.Models;
using System.Text.RegularExpressions;

namespace PhotoCarousel.Services;

public partial class PhotoService(ILogger<PhotoService> logger, IMemoryCache cache) : IPhotoService
{
    private static readonly TimeSpan oneMinute = TimeSpan.FromMinutes(1);
    private static readonly Regex imageExtentions = PhotoExtensions();

    public IEnumerable<PhotoMetadata> GetPhotosCached()
    {
        IEnumerable<PhotoMetadata>? photosCached = cache.GetOrCreate("photos", c =>
        {
            IEnumerable<PhotoMetadata> photos = GetPhotos();
            if (photos is null || !photos.Any())
            {
                return null;
            }

            c.AbsoluteExpirationRelativeToNow = oneMinute;
            return photos;
        });

        return photosCached ?? [];
    }

    public IEnumerable<PhotoMetadata> GetPhotos()
    {
        IEnumerable<string> files = Directory.EnumerateFiles("wwwroot/img/");
        logger.LogInformation("{count} files found in wwwroot/img/", files.Count());

        IEnumerable<PhotoMetadata> metadata = files
            .Where(x => imageExtentions.IsMatch(x))
            .Select(x => new PhotoMetadata(x))
            .Shuffle();

        return metadata;
    }

    [GeneratedRegex(@"\.jpe?g$|\.png$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex PhotoExtensions();
}
