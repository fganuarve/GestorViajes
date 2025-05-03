using GestorViajes.Models;
using ImageMagick.Formats;
using ImageMagick;
using GestorViajes.Models.ViewModels.Image;

namespace GestorViajes.Services.Image
{
	public class ImageProcessor : IImageProcessor
	{
		public async Task<GenericResponse<ImageResizeOutput>> Resize(ImageResizeInput input)
		{
			try
			{
				byte[] imagenBytes = Convert.FromBase64String(input.File);
				using (var image = new MagickImage(imagenBytes))
				{
					using (MemoryStream memStream = new MemoryStream())
					{
						if (image.BaseWidth > input.MaxWidth || image.BaseHeight > input.MaxHeight)
						{
							MagickGeometry geometry = new MagickGeometry(input.MaxWidth, input.MaxHeight);
							geometry.IgnoreAspectRatio = false;
							image.Resize(geometry);
						}

						image.Settings.SetDefines(new JpegWriteDefines { Extent = input.MaxSize });
						image.Format = MagickFormat.Jpeg;
						await image.WriteAsync(memStream);
					}
					return new GenericResponse<ImageResizeOutput>()
					{
						Data = new ImageResizeOutput()
						{
							File = image.ToBase64(),
						}
					};
				}
			}
			catch (Exception ex)
			{
				return new GenericResponse<ImageResizeOutput>() { Error = new ErrorResponse(ex) };
			}

		}
	}
}
