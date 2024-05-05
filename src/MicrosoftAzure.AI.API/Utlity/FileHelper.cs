namespace MicrosoftAzure.AI.API.Utlity
{
    public class FileHelper
    {
        static string[] allowedExtensions = { ".txt" };
        public static bool IsFileExtensionAllowed(string fileName)
        {
            var extension = Path.GetExtension(fileName.ToLower());
            return allowedExtensions.Contains(extension);
        }
    }
}
