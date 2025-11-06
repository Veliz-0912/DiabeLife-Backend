using DevsPros.Diabelife.Platform.API.Shared.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;

public class Recommendation : BaseEntity
{
    public string Text { get; set; } = string.Empty;

    public Recommendation() { }

    public Recommendation(string text)
    {
        Text = text;
    }

    public void UpdateText(string text)
    {
        Text = text;
        UpdatedAt = DateTime.UtcNow;
    }
}