using MPJBS.ViewModels;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;

namespace MPJBS.Common
{
    public static class Utility
    {
        //for viewing report (Bold Report)
        public static List<BoldReports.Models.ReportViewer.ReportParameter> GenerateReportViewerParams(ReportParamViewModel reportParam)
        {
            List<BoldReports.Models.ReportViewer.ReportParameter> parameters = new()
            {
                new()
                {
                    Name = "StartDate",
                    Values = new List<string>() { reportParam.StartDate.ToString()! }
                },
                new()
                {
                    Name = "EndDate",
                    Values = new List<string>() { reportParam.EndDate.ToString()! }
                }
            };
            return parameters;
        }

        public static string SaveImage(IFormFile imageFile, string uploadPath)
        {
            // Check if the image file is not null and the file size is greater than 0
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var image = Image.Load(imageFile.OpenReadStream()))
                {
                    // Calculate the new dimensions for a 3:4 aspect ratio
                    int width = 800;
                    int height = 600;


                    // giving 3:4 Size
                    //int width;
                    //int height;
                    //if (image.Width / 3 > image.Height / 4)
                    //{
                    //    width = image.Height * 3 / 4;
                    //    height = image.Height;
                    //}
                    //else
                    //{
                    //    width = image.Width;
                    //    height = image.Width * 4 / 3;
                    //}
                    // Resize the image
                    image.Mutate(x => x.Resize(width, height));

                    // Save the resized image to the file path
                    using (var outputStream = new FileStream(uploadPath, FileMode.Create))
                    {
                        // Get the encoder for the file format
                        var encoder = GetEncoder(Path.GetExtension(uploadPath));

                        // Save the image using the specified encoder
                        image.Save(outputStream, encoder);
                    }

                    // Return the file path
                    return uploadPath;
                }
            }

            // Return null if the image file is null or empty
            return null;
        }

        private static IImageEncoder GetEncoder(string extension)
        {
            switch (extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return new JpegEncoder();
                case ".png":
                    return new PngEncoder();
                default:
                    // Default to PNG if the file extension is not recognized
                    return new PngEncoder();
            }
        }
    }
}
