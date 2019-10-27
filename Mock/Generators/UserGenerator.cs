using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recademy.Models;

namespace Mock.Generators
{
    public class UserGenerator
    {
        private readonly NameGenerator _nameGenerator = new NameGenerator();

        public User GetUser()
        {
            string name = _nameGenerator.GetName();
            
            int id = Utilities.CurrentUserId;
            
            User result = new User() { Name = name, GithubLink = "https://github.com/InRedikaWB"};

            Utilities.CurrentUserId++;

            return result;
        }
    }
}
