using SocialRofl.Data;
using SocialRofl.Exceptions;
using SocialRofl.Interfaces;
using SocialRofl.Models;
using SocialRofl.Models.Database;

namespace SocialRofl.Logic
{
    public class PhotoLogic
    {
        public const string PhotosDirectory = "/Photos";
        public readonly string[] AllowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp" };
        private IHashGenerator _hashGenerator;
        private readonly DataContext _db;

        public PhotoLogic(IHashGenerator hashGenerator, DataContext db)
        {
            _hashGenerator = hashGenerator;
            _db = db;
        }

        public void UploadFileToDisk(string hash, IFormFile file)
        {
            string path = $"{PhotosDirectory}/{hash}.jpeg";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory("/Photos");
            }
            using var fileStream = new FileStream(path, FileMode.Create);
            file.CopyTo(fileStream);
        }

        public IFormFile Compress(IFormFile file)
        {
            return file;
        }

        public PhotoUploadResult UploadPhoto(IFormFile file, string description, int ownerId)
        {
            if (file == null)
            {
                throw new NullFileException();
            }
            var ext = Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(ext))
            {
                throw new BadExtensionException(AllowedExtensions);
            }
            var hash = _hashGenerator.GetAlphanumRandString(16);
            UploadFileToDisk(hash, Compress(file));
            var me = _db.Users.SingleOrDefault(x => x.Id == ownerId);
            if(me == null)
            {
                throw new UserNotFoundException();
            }
            _db.Photos.Add(new Photo { Description = description, Hash = hash, User = me });
            _db.SaveChanges();
            return new PhotoUploadResult { Hash = hash };
        }

        public FileStream GetPhoto(string hash)
        {
            var photo = _db.Photos.SingleOrDefault(x => x.Hash == hash);
            if (photo == null)
            {
                throw new PhotoNotFoundException();
            }
            var image = File.OpenRead($"{PhotosDirectory}/{hash}.jpeg");
            return image;
        }

        public AllPhotos GetUserPhotos(int userId)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            _db.Entry(user).Collection(x => x.Photos).Load();
            var photos = user.Photos.Select(x => x.Hash).ToList();
            return new AllPhotos { Hashes = photos };
        }
    }
}
