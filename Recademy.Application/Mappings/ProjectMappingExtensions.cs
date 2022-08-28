﻿using System.Linq;
using Recademy.Core.Models.Projects;
using Recademy.Core.Tools;
using Recademy.Dto.Projects;

namespace Recademy.Application.Mappings;

public static class ProjectMappingExtensions
{
    public static ProjectInfoDto ToDto(this ProjectInfo project)
    {
        return new ProjectInfoDto
        {
            ProjectId = project.Id,
            UserId = project.AuthorId,
            ProjectName = project.Title,
            ProjectUrl = project.GithubLink,
            ProjectSkills = project.Skills.Select(skill => skill.ToDto()).ToList(),
        };
    }

    public static ProjectInfo FromDto(this ProjectInfoDto project)
    {
        return new ProjectInfo
        {
            Id = project.ProjectId,
            AuthorId = project.UserId,
            Title = project.ProjectName,
            GithubLink = project.ProjectUrl,
            Skills = project.ProjectSkills.Select(skill => skill.FromDto()).ToList(),
        };
    }
}