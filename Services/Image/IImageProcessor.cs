using GestorViajes.Models.ViewModels.Image;
using GestorViajes.Models;

namespace GestorViajes.Services.Image
{
	public interface IImageProcessor
	{
		Task<GenericResponse<ImageResizeOutput>> Resize(ImageResizeInput input);
	}
}
