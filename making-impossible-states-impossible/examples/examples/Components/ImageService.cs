namespace examples.Components;

public class ImageService
{
    public event EventHandler<ImageEventArgs> ImageReceived;
}

public class ImageEventArgs : EventArgs
{
    public byte[] ImageBytes { get; set; }
    public bool  ErrorRetrievingImage { get; set; }
}