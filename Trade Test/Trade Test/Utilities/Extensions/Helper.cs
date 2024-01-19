namespace Trade_Test.Utilities.Extensions
{
    public static class Helper
    {
        public static string GenrateSixDigitRandomNumber(string prefix = "")
        {
            var randomNo = new Random();
            string result = string.IsNullOrEmpty(prefix) 
                ? randomNo.Next(111111, 999999).ToString() 
                : prefix + "-" + randomNo.Next(111111, 999999);
            return result;
        }

        public static Guid ToGuid(this Guid? source)
        {
            return source ?? Guid.Empty;
        }

        public static string ManageWordCaseSensitivity(string strWord)
        {
            return strWord.ToUpper().Replace(" ", "");
        }

        public static byte[] ConvertImgageToByteArray(string imageLocation)
        {
            byte[] imageData = null;
            FileInfo fileInfo = new(imageLocation);

            if (fileInfo != null && fileInfo.Exists)
            {
                long imageFileLength = fileInfo.Length;
                FileStream fs = new(imageLocation, FileMode.Open, FileAccess.Read);
                BinaryReader br = new(fs);
                imageData = br.ReadBytes((int)imageFileLength);
            }

            return imageData;
        }

        public static void CovertByteArrayToImage(byte[] bytesArr, string destinationPath)
        {
            using (var ms = new MemoryStream(bytesArr))
            {
                using (var fs = new FileStream(destinationPath, FileMode.Create))
                {
                    ms.WriteTo(fs);
                }

            }
        }

        public static byte[] GetBytesFromIFormFile(IFormFile file)
        {
            byte[] mediaContent = { };

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    mediaContent = ms.ToArray();
                }
            }

            return mediaContent;
        }

       

        public static List<string> GetHeadersFromCsv(Stream fileContent)
        {
            List<string> headers = new List<string>();

            using (Microsoft.VisualBasic.FileIO.TextFieldParser csvReader = new Microsoft.VisualBasic.FileIO.TextFieldParser(fileContent))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;

                headers = csvReader.ReadFields().ToList();

            }

            return headers;
        }
        
    }
}
