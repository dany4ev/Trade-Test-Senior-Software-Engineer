using System.Runtime.InteropServices;
using System.Text;


namespace Trade_Test.Utilities.Extensions
{
    public static class FileUploader
    {
        public static void UploadImage(this IFormFileCollection files, string filePath, string finalImageName)
        {
            if (files.Count > 0)
            {
                var file = files[0];
                using (Stream stream = file.OpenReadStream())
                {
                    var imageBytes = ConvertToByteArray(stream);

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        filePath += "\\uploads\\";
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        filePath += "//uploads//";
                    }

                    if (Directory.Exists(filePath))
                    {
                        SaveImageFileStream(imageBytes, filePath, finalImageName);
                    }
                    else
                    {
                        Directory.CreateDirectory(filePath);
                        SaveImageFileStream(imageBytes, filePath, finalImageName);
                    }
                }


            }
        }

        public static void UploadImage(byte[] media, string filePath, string fileName)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                filePath += "\\uploads\\";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                filePath += "//uploads//";
            }


            if (Directory.Exists(filePath))
            {
                SaveImageFileStream(media, filePath, fileName);
            }
            else
            {
                Directory.CreateDirectory(filePath);
                SaveImageFileStream(media, filePath, fileName);
            }
        }

        public static string GetImage(this string file, string filePath)
        {
            var result = string.Empty;
            byte[] imageArray = null;
            if (file != null)
            {

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    filePath += "\\uploads\\" + file;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    filePath += "//uploads//" + file;
                }

                if (File.Exists(filePath))
                {
                    imageArray = System.IO.File.ReadAllBytes(filePath);
                    result = Convert.ToBase64String(imageArray);
                }
                else
                {
                    result = string.Empty;
                }



            }
            return result;
        }

        public static Stream ConvertToBase64(Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return new MemoryStream(Encoding.UTF8.GetBytes(base64));
        }

        public static byte[] ConvertToByteArray(Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            //string base64 = Convert.ToBase64String(bytes);
            return bytes;
        }
        private static bool SaveImageFileStream(byte[] ImageBytes, string ImageFilePath, string IMAGE_NAME)
        {
            bool success = false;
            try
            {
                using (FileStream str = new(Path.Combine(ImageFilePath, IMAGE_NAME), FileMode.Create))
                {
                    str.Write(ImageBytes, 0, Convert.ToInt32(ImageBytes.Length));
                    success = true;

                }
            }
            catch (Exception)
            {

                success = false;
                throw null;
            }
            return success;
        }
    }
}
