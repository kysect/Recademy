using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Recademy.Context;
using Recademy.Dto;
using Recademy.Services.Abstraction;
using ProjectInfo = Recademy.Models.ProjectInfo;

namespace Recademy.Services
{
    public class ProjectService : IProjectService
    {
        public RecademyContext Context;

        public ProjectService(RecademyContext context)
        {
            Context = context;
        }

        public ProjectInfo GetProjectInfo(int projectId)
        {
            return Context.ProjectInfos.SingleOrDefault(k => k.Id == projectId);
        }
    }
}