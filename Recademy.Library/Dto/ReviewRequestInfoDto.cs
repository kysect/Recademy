using System;
using Recademy.Library.Models;
using Recademy.Library.Types;

namespace Recademy.Library.Dto
{
    public class ReviewRequestInfoDto
    {
        public ReviewRequestInfoDto(ReviewRequest request)
        {
            Id = request.Id;
            ProjectId = request.ProjectId;
            DateCreate = request.DateCreate;
            State = request.State;
            ProjectInfo = request.ProjectInfo != null ? new ProjectInfoDto(request.ProjectInfo) : null;
            User = request.User != null ? new UserInfoDto(request.User) : null;
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime DateCreate { get; set; }
        public ProjectState State { get; set; }
        public ProjectInfoDto ProjectInfo { get; set; }
        public UserInfoDto User { get; set; }

        //TODO: Need to rework coz of multi reviewers
        //public ReviewResponse ReviewResponse { get; set; }
    }
}