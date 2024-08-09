using PhotoCarousel.Models;

namespace PhotoCarousel.Interfaces;

public interface IPhotoService
{
    IEnumerable<PhotoMetadata> GetPhotosCached();
    IEnumerable<PhotoMetadata> GetPhotos();
}