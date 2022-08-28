using Recademy.Dto.Enums;
using Recademy.Dto.Projects;
using Recademy.Dto.Users;

using System;

namespace Recademy.Dto.Reviews
{
    public class ReviewRequestInfoDto
    {
        public ReviewRequestInfoDto()
        {
        }

        public ReviewRequestInfoDto(int id, int projectId, DateTime dateCreate, ProjectStateDto state, ProjectInfoDto projectInfo, RecademyUserDto user)
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
        public ProjectStateDto State { get; init; }
        public ProjectInfoDto ProjectInfo { get; init; }
        public RecademyUserDto User { get; init; }

        //TODO: Need to rework coz of multi reviewers
        //public ReviewResponse ReviewResponse { get; set; }
    }
}