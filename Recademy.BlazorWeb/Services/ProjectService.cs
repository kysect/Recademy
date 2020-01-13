using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recademy.BlazorWeb.Context;
using Recademy.BlazorWeb.Models;
using Recademy.BlazorWeb.Services.Abstraction;
using Recademy.BlazorWeb.Types;

namespace Recademy.BlazorWeb.Services
{
    public class ProjectService : IProjectService
    {
        private readonly RecademyContext _context;

        public ProjectService(RecademyContext context)
        {
            _context = context;
        }

        public ProjectInfo GetProjectInfo(int projectId)
        {
            var project = _context
                .ProjectInfos
                .Include(s => s.Skills)
                .FirstOrDefault(k => k.Id == projectId);

            if (project == null)
            {
                throw new RecademyException("No project with current id!");
            }

            return project;
        }
    }
}