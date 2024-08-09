using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoCarousel.Interfaces;
using PhotoCarousel.Models;
using System.Text.Json;

namespace PhotoCarousel.Pages;

public class IndexModel(ILogger<IndexModel> logger, IPhotoService photoService) : PageModel
{
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

    [BindProperty]
    public string Metadata { get; private set; } = string.Empty;

    public void OnGet()
    {
        logger.LogInformation("Retrieving photos");
        IEnumerable<PhotoMetadata> photos = photoService.GetPhotosCached();
        logger.LogInformation("{count} photos retrieved", photos.Count());
        Metadata = JsonSerializer.Serialize(photos, options) ?? string.Empty;
    }
}
