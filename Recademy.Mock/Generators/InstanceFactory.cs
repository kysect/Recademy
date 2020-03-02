using System.Linq;
using Recademy.Library.Dto;

namespace Recademy.Mock.Generators
{
    public static class InstanceFactory
    {
        public static string GenerateString(int size = 15)
        {
            const string chars = "qwertyuiopasdfghjklzxcvbnmABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            char[] res = Enumerable
                .Range(1, size)
                .Select(s => chars[Utilities.Random.Next(chars.Length)])
                .ToArray();
            
            return new string(res);
        }

        public static AddProjectDto CreateAddProjectDto(int userId)
        {
            return new AddProjectDto(GenerateString(), userId, GenerateString());
        }
    }
}