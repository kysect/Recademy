using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using Recademy.Api.Services.Abstraction;
using Recademy.Api.Tools;

namespace Recademy.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/authorize")]
    [ApiController]
    public class GithubAuthorizationController : Controller
    {
        private readonly IGithubApiAccessor _githubApi;

        public GithubAuthorizationController(IGithubApiAccessor githubApi)
        {
            _githubApi = githubApi;
        }

        /// <summary>
        /// Get oauth github redirect page
        /// </summary>
        /// <returns></returns>
        [HttpGet("getRedirectPage")]
        public Uri GetRedirectPage()
        {
            return _githubApi.GetGithubOauthRedirectPage();
        }

        /// <summary>
        /// Callback function for github application
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("getOauthToken")]
        public OauthToken GetGithubOauthToken([Required] string code)
        {
            return _githubApi.GetOauthToken(code);
        }
    }
}
