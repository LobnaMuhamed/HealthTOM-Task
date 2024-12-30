namespace Web_API.Interfaces
{
	public interface IImageService
	{
		Task<string> SaveImageAsync(IFormFile imageFile, string folderPath);

		void DeleteImage(string imageName, string folderPath);

		string GetImageURL(string imageName, string folderPath);
	}
}
