using Recademy.Models;

namespace Mock.Generators
{
    public class TypesGenerator
    {
        private readonly PrimitiveGenerator _primitiveGenerator = new PrimitiveGenerator();

        public User GetUser()
        {
            Utilities.CurrentUserId++;

            return new User
            {
                Name = _primitiveGenerator.GetName(),
                GithubLink = "https://github.com/InRedikaWB"
            };
        }
    }
}