// namespace examples.Components;
//
// public class LicensePlateTestViewModel(ImageService imageService, LicensePlateService licensePlateService)
// {
//     private ImageService _imageService = imageService;
//     private LicensePlateService _licensePlateService = licensePlateService;
//     
//     public byte[] ImageBytes = [];
//     public ImageState imageState;
//     
//     public string? LicensePlateText;
//     private LprState lprState = LprState.Received;
//     private LicensePlateError? LprErrorReason;
//
//     
//     public void Init()
//     {
//         imageState = ImageState.Loading;
//         _imageService.ImageReceived += OnImageReceived;
//     }
//     
//     public async Task GetLicensePlate() 
//     {
//         try
//         {
//             lprState = LprState.Loading;
//             LicensePlateText = await _licensePlateService.GetLicensePlateFromImage(ImageBytes);
//             lprState = LprState.Received;
//         }
//         catch (LicensePlateException ex)
//         {
//             lprState = LprState.Error;
//             LprErrorReason = ex.ErrorReason;
//         }
//         catch (Exception ex)
//         {
//             lprState = LprState.Error;
//             LprErrorReason = LicensePlateError.Generic;
//         }
//     }
//     
//     private void OnImageReceived(object? sender, ImageEventArgs e)
//     {
//         imageState = e.ErrorRetrievingImage ? ImageState.Error : ImageState.Received;
//         ImageBytes = e.ImageBytes;
//     }
//     
//     public enum ImageState
//     {
//         Loading,
//         Error,
//         Received
//     }
//
//     public enum LprState
//     {
//         Loading,
//         Error,
//         Received
//     }
// }