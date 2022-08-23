using System;
using Recademy.Core.Models;
using Recademy.Core.Tools;
using Recademy.Core.Types;

namespace Recademy.Core.Dto
{
    public class ReviewRequestInfoDto
    {
        public ReviewRequestInfoDto()
        {
            
        }

        public ReviewRequestInfoDto(ReviewRequest request)
        {
            Id = request.Id;
            ProjectId = request.ProjectId;
            DateCreate = request.DateCreate;
            State = request.State;
            ProjectInfo = request.ProjectInfo.Maybe(p => new ProjectInfoDto(p));
            User = request.User.Maybe(u => new UserInfoDto(u));
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