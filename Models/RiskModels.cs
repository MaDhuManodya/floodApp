namespace FloodApp.Models;

public enum RiskLevel
{
    Low,
    Medium,
    High,
    Unknown
}

public class RiskResult
{
    public RiskLevel Level { get; set; }
    public string Message { get; set; } = "";
    public string ColorCode { get; set; } = "#10B981"; // Default green for Low
    public double Score { get; set; }

    public static RiskResult Default => new() 
    { 
        Level = RiskLevel.Unknown, 
        Message = "Select a location to check risk.", 
        ColorCode = "#6B7280" 
    };
}
