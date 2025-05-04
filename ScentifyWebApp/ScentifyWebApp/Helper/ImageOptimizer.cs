using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;
using System.Linq;

namespace ScentifyWebApp.Helper
{
    public static class ImageOptimizer
    {
        public static string CompressAndSaveImage(string base64Image, string imageName, string saveDirectory)
        {
            // Convert the base64 string to a byte array
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            using (var image = Image.Load<Rgba32>(imageBytes)) // Ensure RGBA to preserve alpha
            {
                Directory.CreateDirectory(saveDirectory);
                string filePath = Path.Combine(saveDirectory, imageName + ".webp");

                var webpEncoder = new WebpEncoder
                {
                    Quality = 100,
                    FileFormat = WebpFileFormatType.Lossless, // Lossless preserves transparency
                };

                image.Save(filePath, webpEncoder); // Save with WebP encoder

                return filePath;
            }
        }

        public static string SaveAsPng(string base64Image, string imageName, string saveDirectory)
        {
            if (base64Image.StartsWith("data:image"))
            {
                base64Image = base64Image.Split(',')[1];
            }

            byte[] imageBytes = Convert.FromBase64String(base64Image);

            using (var image = Image.Load<Rgba32>(imageBytes))
            {
                Directory.CreateDirectory(saveDirectory);
                string filePath = Path.Combine(saveDirectory, imageName + ".png");

                image.Save(filePath); // Automatically uses PNG format

                return filePath;
            }
        }

        public static void CompressAndSaveAllImages(string inputDirectory, string outputDirectory)
        {
            if (!Directory.Exists(inputDirectory))
            {
                Console.WriteLine("Input directory does not exist.");
                return;
            }

            Directory.CreateDirectory(outputDirectory);

            var supportedExtensions = new[] { ".png", ".jpg", ".jpeg", ".webp" };
            var files = Directory.GetFiles(inputDirectory)
                                 .Where(f => supportedExtensions.Contains(Path.GetExtension(f).ToLower()));

            foreach (var file in files)
            {
                Console.WriteLine($"Processing: {file}");

                using (var image = Image.Load<Rgba32>(file)) // Ensure alpha is loaded
                {
                    var imageName = Path.GetFileNameWithoutExtension(file);
                    var outputPath = Path.Combine(outputDirectory, imageName + ".webp");

                    var webpEncoder = new WebpEncoder
                    {
                        FileFormat = WebpFileFormatType.Lossy,
                        Quality = 70
                    };

                    image.Save(outputPath, webpEncoder);
                    Console.WriteLine($"Saved to: {outputPath}");
                }
            }

            Console.WriteLine("All images compressed using WebP Lossy (transparency preserved).");
        }
    }
}
