using System;
using Recademy.Core.Models.Reviews;
using Recademy.Core.Tools;
using Recademy.Core.Types;
using Recademy.Shared.Dtos.Projects;
using Recademy.Shared.Dtos.Users;

namespace Recademy.Shared.Dtos.Reviews
{
    public class ReviewRequestInfoDto
    {
        public ReviewRequestInfoDto()
        {
        }

        public ReviewRequestInfoDto(int id, int projectId, DateTime dateCreate, ProjectState state, ProjectInfoDto projectInfo, RecademyUserDto user)
        {
            Id = id;
            ProjectId = projectId;
            DateCreate = dateCreate;
            State = state;
            ProjectInfo = projectInfo;
            User = user;
        }

        public int Id { get; init; }
        public int ProjectId { get; init; }
        public DateTime DateCreate { get; init; }
        public ProjectState State { get; init; }
        public ProjectInfoDto ProjectInfo { get; init; }
        public RecademyUserDto User { get; init; }

        //TODO: Need to rework coz of multi reviewers
        //public ReviewResponse ReviewResponse { get; set; }
    }
}