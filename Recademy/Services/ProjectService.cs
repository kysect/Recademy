using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Recademy.Context;
using Recademy.Dto;
using Recademy.Services.Abstraction;
using ProjectInfo = Recademy.Models.ProjectInfo;

namespace Recademy.Services
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
            return _context
                .ProjectInfos
                .Include(s => s.Skills)
                .FirstOrDefault(k => k.Id == projectId);
        }
    }
}