namespace examples.Components;

public class LicensePlateService
{
    public Task<LicensePlateResult> GetLicensePlateFromImage(string path)
    {
        return Task.FromResult(new LicensePlateResult
        {
            Text = "POLICE"
        });
    }
}

public record LicensePlateResult
{
    public string Text { get; init; }
}