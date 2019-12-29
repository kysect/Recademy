using Recademy.Models;

namespace Mock.Generators
{
    public class UserGenerator
    {
        private readonly PrimitiveGenerator _primitiveGenerator = new PrimitiveGenerator();

        public User GetUser()
        {
            string name = _primitiveGenerator.GetName();

            User result = new User
            {
                Name = name,
                GithubLink = "https://github.com/InRedikaWB"
            };

            Utilities.CurrentUserId++;

            return result;
        }
    }
}