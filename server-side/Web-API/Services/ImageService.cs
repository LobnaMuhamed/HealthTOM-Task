using Web_API.Interfaces;

namespace Web_API.Services
{
	public class ImageService : IImageService
	{
		private readonly IWebHostEnvironment _environment;
		public ImageService(IWebHostEnvironment environment)
		{
			_environment = environment;
		}
		public async Task<string> SaveImageAsync(IFormFile imageFile, string folderPath)
		{
			if (imageFile == null || imageFile.Length == 0)
				throw new ArgumentException("Invalid image file");
			string uniqueFileName = Guid.NewGuid().ToString()
				+ Path.GetExtension(imageFile.FileName);
			string fullPath = Path.Combine(_environment.WebRootPath, folderPath, uniqueFileName);

			Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, folderPath));
			using (var fileStream = new FileStream(fullPath, FileMode.Create))
			{
				await imageFile.CopyToAsync(fileStream);
			}
			return uniqueFileName;
		}

		public void DeleteImage(string imageName, string folderPath)
		{
			if (string.IsNullOrWhiteSpace(imageName) || string.IsNullOrWhiteSpace(folderPath))
				throw new ArgumentException("Image name and folder path must not be null or empty.");

			string fullPath = Path.Combine(_environment.WebRootPath, folderPath, imageName);
			if (File.Exists(fullPath))
				File.Delete(fullPath);
		}

		public string GetImageURL(string imageName, string folderPath)
		{
			if (string.IsNullOrWhiteSpace(imageName) || string.IsNullOrWhiteSpace(folderPath))
				throw new ArgumentException("Image name and folder path must not be null or empty.");

			return Path.Combine(folderPath, imageName).Replace("\\", "/");

		}


	}

}
