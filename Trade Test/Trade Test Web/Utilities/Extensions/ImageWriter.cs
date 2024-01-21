namespace Trade_Test.Utilities.Extensions
{
    public class ImageWriter : IImageWriter
    {
        public string GetImageString(IFormFile file)
        {
            byte[] fileBytes;
            using var ms = new MemoryStream();

            if (file == null) return "";

            file.CopyTo(ms);
            fileBytes = ms.ToArray();
            string imageB64String;

            if (CheckIfImageFile(fileBytes))
            {
                // NOTE: get image base64 string               
                imageB64String = Convert.ToBase64String(fileBytes);
            }
            else
            {
                imageB64String = "Invalid image file";
            }

            return imageB64String;
        }


        private bool CheckIfImageFile(byte[] fileBytes)
        {
            return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
        }
    }
}
