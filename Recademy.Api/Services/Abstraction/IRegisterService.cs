using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recademy.Library.Models;

namespace Recademy.Api.Services.Abstraction
{
    public interface IRegisterService
    {
        public void Register(User user);
        public User GetUserFromClaims(ClaimsPrincipal claims);
        public bool IsUserRegistered(User user);
    }
}
