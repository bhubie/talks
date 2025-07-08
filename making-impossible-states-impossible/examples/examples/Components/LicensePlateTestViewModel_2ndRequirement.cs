namespace examples.Components;

// public class LicensePlateTestViewModel(ImageService imageService, LicensePlateService licensePlateService)
// {
//     private ImageService _imageService = imageService;
//     private LicensePlateService _licensePlateService = licensePlateService;
//     
//     public byte[] ImageBytes = [];
//     public bool IsLoadingImage;
//     public bool ErrorFetchingImage;
//     
//     public string? LicensePlateText;
//     public bool IsLoadingLpr;
//     public bool IsLprError;
//
//     public void Init()
//     {
//         IsLoadingImage = true;
//         _imageService.ImageReceived += OnImageReceived;
//     }
//     
//     public async Task GetLicensePlate() 
//     {
//         try
//         { 
//             IsLoadingLpr = true;
//             LicensePlateText = await _licensePlateService.GetLicensePlateFromImage(ImageBytes);
//             IsLoadingLpr = false;
//
//         }
//         catch (Exception ex)
//         {
//             IsLoadingLpr = false;
//             IsLprError = true;
//         }
//     }
//     
//     private void OnImageReceived(object? sender, ImageEventArgs e)
//     {
//         IsLoadingImage = false;
//         ImageBytes = e.ImageBytes;
//         ErrorFetchingImage = e.ErrorRetrievingImage;
//     }
// }