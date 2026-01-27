namespace FloodApp.Models;

public record LatLng(double Lat, double Lng);

public class Province
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public LatLng Coords { get; set; } = new(0, 0);
    public List<District> Districts { get; set; } = new();
}

public class District
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int ProvinceId { get; set; }
    public LatLng Coords { get; set; } = new(0, 0);
    public List<Town> Towns { get; set; } = new();
}

public class Town
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int DistrictId { get; set; }
    public LatLng Coords { get; set; } = new(0, 0);
}
