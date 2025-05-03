namespace GestorViajes.Models.ViewModels.Image
{
    public class ImageResizeInput
    {
        public string File { get; set; }
        public uint MaxWidth { get; set; }
        public uint MaxHeight { get; set; }
        public int? MaxSize { get; set; }
    }
}
