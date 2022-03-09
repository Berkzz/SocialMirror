namespace SocialRofl.Logic
{
    public class PhotoUploader
    {
        public void Upload(string hash, IFormFile file)
        {
            string path = $"{Environment.CurrentDirectory}/Photos/{hash}.jpeg";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory($"{Environment.CurrentDirectory}/Photos");
            }
            using var fileStream = new FileStream(path, FileMode.Create);
            file.CopyTo(fileStream);
        }
    }
}
