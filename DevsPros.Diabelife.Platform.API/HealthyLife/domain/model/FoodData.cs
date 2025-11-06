using DevsPros.Diabelife.Platform.API.Shared.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;

public class FoodData : BaseEntity
{
    public string Food { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }

    public FoodData() 
    {
        Timestamp = DateTime.UtcNow;
    }

    public FoodData(string food)
    {
        Food = food;
        Timestamp = DateTime.UtcNow;
    }

    public FoodData(string food, DateTime timestamp)
    {
        Food = food;
        Timestamp = timestamp;
    }

    public void UpdateFood(string food)
    {
        Food = food;
        UpdatedAt = DateTime.UtcNow;
    }
}