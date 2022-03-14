using MlkPwgen;
using SocialRofl.Interfaces;

namespace SocialRofl.Logic
{
    public class HashGenerator : IHashGenerator
    {
        public string GetAlphanumRandString(int length)
        {
            return PasswordGenerator.Generate(length: length, allowed: Sets.Alphanumerics);
        }
    }
}
