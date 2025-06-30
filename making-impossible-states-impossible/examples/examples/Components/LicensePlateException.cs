namespace examples.Components;

public class LicensePlateException : Exception
{
    public LicensePlateError ErrorReason { get; set; }
}

public enum LicensePlateError
{
    NotLicensed,
    ImageNotFound,
    Generic
}