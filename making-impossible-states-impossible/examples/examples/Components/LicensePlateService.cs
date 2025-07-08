namespace examples.Components;

public class LicensePlateService
{
    public Task<string> GetLicensePlateFromImage(byte[] imagaeBytes)
    {
        return Task.FromResult("Police");
    }
}

public record LicensePlateResult
{
    public string Text { get; init; }
}