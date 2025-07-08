// namespace examples.Components;
//
// public class LicensePlateTestViewModel(ImageService imageService)
// {
//     private ImageService _imageService = imageService;
//
//     public byte[] ImageBytes = [];
//     public bool IsLoadingImage;
//     public bool ErrorFetchingImage;
//
//     public void Init()
//     {
//         IsLoadingImage = true;
//         _imageService.ImageReceived += OnImageReceived;
//     }
//     
//     private void OnImageReceived(object? sender, ImageEventArgs e)
//     {
//         IsLoadingImage = false;
//         ImageBytes = e.ImageBytes;
//         ErrorFetchingImage = e.ErrorRetrievingImage;
//     }
// }