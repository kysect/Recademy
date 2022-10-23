using Recademy.Dto.Enums;
using Recademy.Dto.Projects;
using System;

namespace Recademy.Dto.Reviews;

public class ReviewRequestInfoDto
{
    public ReviewRequestInfoDto()
    {
    }

    public ReviewRequestInfoDto(int id, int projectId, DateTime creationDate, ProjectStateDto state, ProjectInfoDto projectInfo, int userId, string username)
    {
        Id = id;
        ProjectId = projectId;
        CreationDate = creationDate;
        State = state;
        ProjectInfo = projectInfo;
        UserId = userId;
        Username = username;
    }

    public int Id { get; init; }
    public int ProjectId { get; init; }
    public DateTime CreationDate { get; init; }
    public ProjectStateDto State { get; init; }
    public ProjectInfoDto ProjectInfo { get; init; }
    public int UserId { get; init; }
    public string Username { get; init; }

    //TODO: Need to rework coz of multi reviewers
    //public ReviewResponse ReviewResponse { get; set; }
}