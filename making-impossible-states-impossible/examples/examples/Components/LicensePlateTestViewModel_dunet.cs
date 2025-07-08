using Dunet;

namespace examples.Components;

public class LicensePlateTestViewModel(ImageService imageService, LicensePlateService licensePlateService)
{
    private ImageService _imageService = imageService;
    private LicensePlateService _licensePlateService = licensePlateService;
    
    public LicensePlateTestState LicensePlateTestState;
    
    public void Init()
    {
        LicensePlateTestState = new LicensePlateTestState.LoadingImage();
        _imageService.ImageReceived += OnImageReceived;
    }
    
    private void OnImageReceived(object? sender, ImageEventArgs e)
    {
        if (e.ErrorRetrievingImage)
        {
            LicensePlateTestState = new LicensePlateTestState.ErrorRetrievingImage();
        }
        else
        {
            LicensePlateTestState = new LicensePlateTestState.DisplayingImage(e.ImageBytes,  _licensePlateService);
        }
    }
}

[Union]
public partial record LicensePlateTestState
{
    partial record LoadingImage;
    partial record ErrorRetrievingImage;

    public partial record DisplayingImage(byte[] ImageBytes, LicensePlateService _licensePlateService)
    {
        public  LicensePlateRecognitionState LprState = new LicensePlateRecognitionState.Unknown();
        public async Task GetLicensePlate()
        {
            try
            {
                LprState = new LicensePlateRecognitionState.Loading();
                var licensePlateText = await _licensePlateService.GetLicensePlateFromImage(ImageBytes);
                LprState = new LicensePlateRecognitionState.Received(licensePlateText);
            }
            catch (LicensePlateException ex)
            {
                LprState = new LicensePlateRecognitionState.Error(ex.ErrorReason);
            }
            catch (Exception ex)
            {
                LprState = new LicensePlateRecognitionState.Unknown();
            }
        }
    };
}

[Union]
public partial record LicensePlateRecognitionState
{
    partial record Unknown;
    partial record Loading;
    partial record Received(string licensePlateText);
    partial record Error(LicensePlateError errorReason);
}