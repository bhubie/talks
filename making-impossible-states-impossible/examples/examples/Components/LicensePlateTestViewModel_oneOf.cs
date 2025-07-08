using Dunet;
using OneOf;

namespace examples.Components;
public class LicensePlateTestViewModel(ImageService imageService, LicensePlateService licensePlateService)
{
    private ImageService _imageService = imageService;
    private LicensePlateService _licensePlateService = licensePlateService;
    
    public OneOf<LoadingImage, ErrorRetrievingImage, DisplayingImage>  LicensePlateTestState;
    
    public void Init()
    {
        LicensePlateTestState = new LoadingImage();
        _imageService.ImageReceived += OnImageReceived;
    }
    
    private void OnImageReceived(object? sender, ImageEventArgs e)
    {
        if (e.ErrorRetrievingImage)
        {
            LicensePlateTestState = new ErrorRetrievingImage();
        }
        else
        {
            LicensePlateTestState = new DisplayingImage(e.ImageBytes,  _licensePlateService);
        }
    }
}

public record LoadingImage;
public record ErrorRetrievingImage;

public record DisplayingImage(byte[] ImageBytes, LicensePlateService _licensePlateService)
{
    public  OneOf<LprUnknown, LprLoading, LprReceived, LprError> LprState = new LprUnknown();
    public async Task GetLicensePlate()
    {
        try
        {
            LprState = new LprLoading();
            var licensePlateText = await _licensePlateService.GetLicensePlateFromImage(ImageBytes);
            LprState = new LprReceived(licensePlateText);
        }
        catch (LicensePlateException ex)
        {
            LprState = new LprError(ex.ErrorReason);
        }
        catch (Exception ex)
        {
            LprState = new LprUnknown();
        }
    }
};

public record LprUnknown;
public record LprLoading;
public record LprReceived(string licensePlateText);
public record LprError(LicensePlateError errorReason);