namespace examples.Components;

public class LicensePlateService
{
    public Task<LicensePlateResult> GetLicensePlateFromImage(byte[] imagaeBytes)
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