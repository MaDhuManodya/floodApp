using System.Text.Json;

namespace FloodApp.Services;

public class GeoJsonService
{
    private readonly IWebHostEnvironment _env;
    private string? _cachedGeoJson;

    public GeoJsonService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> GetRiskZonesGeoJsonAsync()
    {
        if (_cachedGeoJson != null) return _cachedGeoJson;

        var path = Path.Combine(_env.WebRootPath, "data", "risk_zones.geojson");
        if (File.Exists(path))
        {
            _cachedGeoJson = await File.ReadAllTextAsync(path);
            return _cachedGeoJson;
        }

        return "{}";
    }
}
