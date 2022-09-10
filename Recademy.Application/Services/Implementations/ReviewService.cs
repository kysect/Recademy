using Recademy.Application.Mappings;
using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Reviews;
using Recademy.Core.Types;
using Recademy.DataAccess;
using Recademy.Dto.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recademy.Application.Services.Implementations;

public class ReviewService : IReviewService
{
    private readonly RecademyContext _context;

    public ReviewService(RecademyContext context)
    {
        _context = context;
    }

    public IReadOnlyCollection<ReviewRequestInfoDto> GetReviewRequests()
    {
        return _context.ReviewRequests
            .Where(s => s.State == ProjectState.Requested)
            .Select(request => request.ToDto())
            .ToList();
    }

    public IReadOnlyCollection<ReviewRequestInfoDto> ReadReviewRequestBySearchContext(ReviewRequestSearchContextDto searchContextDto)
    {
        ArgumentNullException.ThrowIfNull(searchContextDto);

        var userSkills = _context.RecademyUsers
            .Single(s => s.UserId == searchContextDto.UserId)
            .UserSkills
            .Select(s => s.SkillName)
            .ToHashSet();

        IQueryable<ReviewRequest> query = _context.ReviewRequests
            .Where(request => request.State == ProjectState.Requested || (request.State == ProjectState.Reviewed && !searchContextDto.WithoutReviewed))
            .Where(request => request.ProjectInfo.Skills.All(projectSkill => userSkills.Contains(projectSkill.SkillName)));

        if (searchContextDto.AuthorId != null)
            query = query.Where(request => request.UserId == searchContextDto.AuthorId);

        if (searchContextDto.ProjectName != null)
            query = query.Where(request => request.ProjectInfo.Title.Contains(searchContextDto.ProjectName));

        if (searchContextDto.Tags != null && searchContextDto.Tags.Any())
            query = query.Where(request => request.ProjectInfo.Skills.Any(projectSkill => searchContextDto.Tags.Contains(projectSkill.SkillName)));

        return query
            .Select(request => request.ToDto())
            .ToList();
    }

    public ReviewRequestInfoDto AddReviewRequest(ReviewRequestAddDto reviewRequestAddDto)
    {
        ArgumentNullException.ThrowIfNull(reviewRequestAddDto);

        ProjectInfo project = _context.ProjectInfos
            .SingleOrDefault(project => project.Id == reviewRequestAddDto.ProjectId);

        IReadOnlyCollection<ReviewRequest> projectReviewRequest = _context.ReviewRequests
            .Where(r => r.ProjectId == project.Id)
            .ToList();

        ReviewRequest lastActiveReview = projectReviewRequest
            .FirstOrDefault(request => request.State is ProjectState.Requested or ProjectState.Reviewed);

        if (lastActiveReview != null)
        {
            throw new RecademyException($"Review for this project already exist. Close it before adding new. Review id: {lastActiveReview.Id}");
        }

        //TODO: check if project belong to review author

        var newRequest = new ReviewRequest
        {
            DateCreate = DateTime.UtcNow,
            State = ProjectState.Requested,
            Description = reviewRequestAddDto.Description,
            ProjectId = reviewRequestAddDto.ProjectId,
            UserId = reviewRequestAddDto.UserId,
        };

        _context.Add(newRequest);
        _context.SaveChanges();

        return newRequest.ToDto();
    }

    public ReviewRequestInfoDto CompleteReview(int requestId)
    {
        ReviewRequest request = _context.ReviewRequests.Single(r => r.Id == requestId);

        if (request.State == ProjectState.Requested)
            throw new RecademyException($"Completing review failed. Review request was not reviewed. Review request id: {request.Id}");

        request.State = ProjectState.Completed;
        _context.Update(request);
        _context.SaveChanges();

        return request.ToDto();
    }

    public ReviewRequestInfoDto AbandonReview(int requestId)
    {
        ReviewRequest request = _context.ReviewRequests.Single(r => r.Id == requestId);

        //TODO: check state
        request.State = ProjectState.Abandoned;
        _context.Update(request);
        _context.SaveChanges();

        return request.ToDto();
    }

    public ReviewRequestInfoDto GetReviewInfo(int requestId)
    {
        return _context.ReviewRequests
            .Single(r => r.Id == requestId)
            .ToDto();
    }
}