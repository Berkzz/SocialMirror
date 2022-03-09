using SocialRofl.Data;
using SocialRofl.Models.Database;

namespace SocialRofl.Logic
{
    public class AttachmentChecker
    {
        private DataContext _db;

        public AttachmentChecker(DataContext db)
        {
            _db = db;
        }

        public bool Exists(AttachmentType type, string hash)
        {
            try
            {
                switch (type)
                {
                    case AttachmentType.Photo:
                        return PhotoExists(hash);
                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool PhotoExists(string hash)
        {
            return _db.Photos.Any(x => x.Hash == hash);
        }
    }
}
