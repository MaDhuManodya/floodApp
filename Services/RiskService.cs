using FloodApp.Models;
using System.Text.Json;

namespace FloodApp.Services;

public class RiskService
{
    private readonly GeoJsonService _geoJsonService;

    public RiskService(GeoJsonService geoJsonService)
    {
        _geoJsonService = geoJsonService;
    }

    public async Task<RiskResult> CalculateRiskAsync(LatLng point)
    {
        // 1. Load polygons
        var geoJson = await _geoJsonService.GetRiskZonesGeoJsonAsync();
        
        using var doc = JsonDocument.Parse(geoJson);
        var root = doc.RootElement;
        
        // 2. Iterate features and check point inside
        if (root.TryGetProperty("features", out var features))
        {
            foreach (var feature in features.EnumerateArray())
            {
                var geometry = feature.GetProperty("geometry");
                var type = geometry.GetProperty("type").GetString();
                
                if (type == "Polygon")
                {
                    var coords = geometry.GetProperty("coordinates");
                    // Assuming simple polygon (first ring is outer)
                    var ring = coords[0];
                    var polygon = new List<LatLng>();
                    
                    foreach (var p in ring.EnumerateArray())
                    {
                        polygon.Add(new LatLng(p[1].GetDouble(), p[0].GetDouble())); // GeoJSON is [lng, lat]
                    }

                    if (IsPointInPolygon(point, polygon))
                    {
                        var props = feature.GetProperty("properties");
                        var levelStr = props.GetProperty("riskLevel").GetString();
                        
                         // Match risk level
                        RiskLevel level = Enum.TryParse<RiskLevel>(levelStr, true, out var r) ? r : RiskLevel.Unknown;
                        string color = level switch 
                        {
                            RiskLevel.High => "#EF4444",
                            RiskLevel.Medium => "#F59E0B",
                            RiskLevel.Low => "#10B981",
                            _ => "#6B7280"
                        };
                        
                        return new RiskResult 
                        { 
                            Level = level, 
                            Message = $"Located in {props.GetProperty("description").GetString()}",
                            ColorCode = color,
                            Score = level == RiskLevel.High ? 9.5 : (level == RiskLevel.Medium ? 5.0 : 1.0)
                        };
                    }
                }
            }
        }

        // If not in any zone, assume Safe
        return new RiskResult 
        { 
            Level = RiskLevel.Low, 
            Message = "No direct flood risk detected in this zone.", 
            ColorCode = "#10B981",
            Score = 0.5
        };
    }

    // Ray Casting Algorithm
    private bool IsPointInPolygon(LatLng point, List<LatLng> polygon)
    {
        bool inside = false;
        int j = polygon.Count - 1;
        
        for (int i = 0; i < polygon.Count; i++)
        {
            if ( (polygon[i].Lng > point.Lng) != (polygon[j].Lng > point.Lng) &&
                 (point.Lat < (polygon[j].Lat - polygon[i].Lat) * (point.Lng - polygon[i].Lng) / (polygon[j].Lng - polygon[i].Lng) + polygon[i].Lat) )
            {
                inside = !inside;
            }
            j = i;
        }
        return inside;
    }
}
