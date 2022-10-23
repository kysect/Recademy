using Microsoft.EntityFrameworkCore;
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
using System.Threading.Tasks;

namespace Recademy.Application.Services.Implementations;

public class ReviewService : IReviewService
{
    private readonly RecademyContext _context;

    public ReviewService(RecademyContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<ReviewRequestInfoDto>> GetReviewRequests()
    {
        return await _context.ReviewRequests
            .Where(s => s.State == ReviewState.Requested)
            .Select(request => request.ToDto())
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<ReviewRequestInfoDto>> GetReviewRequestsByUserId(int userId)
    {
        return await _context.ReviewRequests
            .Where(s => s.UserId == userId)
            .Where(s => s.State == ReviewState.Requested)
            .Select(request => request.ToDto())
            .ToListAsync();
    }

    public ReviewRequestInfoDto GetReviewRequestById(int requestId)
    {
        return _context.ReviewRequests
            .Single(r => r.Id == requestId)
            .ToDto();
    }

    public async Task<ReviewRequestInfoDto> CreateReviewRequest(CreateReviewRequestDto createReviewRequestDto)
    {
        ArgumentNullException.ThrowIfNull(createReviewRequestDto);

        ProjectInfo project = _context.ProjectInfos
            .SingleOrDefault(project => project.Id == createReviewRequestDto.ProjectId);

        if (project is null)
            throw new RecademyException($"Project with id {createReviewRequestDto.ProjectId} was not found");

        IReadOnlyCollection<ReviewRequest> reviewRequests = _context.ReviewRequests
            .Where(request => request.ProjectId == project.Id)
            .ToList();

        ReviewRequest lastActiveReview = reviewRequests
            .FirstOrDefault(request => request.State is ReviewState.Requested or ReviewState.Reviewed);

        if (lastActiveReview != null)
            throw new RecademyException($"Review for this project already exist. Close it before adding new. Review id: {lastActiveReview.Id}");

        //TODO: check if project belong to review author

        var newRequest = new ReviewRequest
        {
            State = ReviewState.Requested,
            Description = createReviewRequestDto.Comment,
            CreationTime = DateTime.UtcNow,
            ProjectId = createReviewRequestDto.ProjectId,
            UserId = createReviewRequestDto.UserId,
        };

        _context.ReviewRequests.Add(newRequest);
        await _context.SaveChangesAsync();

        // To set User entity value. TODO: research include.
        newRequest = await _context.ReviewRequests
            .Include(request => request.User)
            .FirstAsync(request => request.Id == newRequest.Id);

        return newRequest.ToDto();
    }

    public ReviewResponseInfoDto CreateReviewResponse(CreateReviewResponseDto createReviewResponseDto)
    {
        ArgumentNullException.ThrowIfNull(createReviewResponseDto);

        ReviewRequest request = _context
            .ReviewRequests
            .Include(s => s.ProjectInfo)
            .ThenInclude(p => p.Skills)
            .Include(s => s.User)
            .FirstOrDefault(r => r.Id == createReviewResponseDto.RequestId);

        if (request == null)
            throw RecademyException.ReviewRequestNotFound(createReviewResponseDto.RequestId);

        if (request.State is ReviewState.Completed or ReviewState.Abandoned)
            throw new RecademyException("Failed to send review. It is already closed.");

        ReviewResponse newReview = createReviewResponseDto.FromDto();

        request.State = ReviewState.Reviewed;

        _context.ReviewResponses.Add(newReview);
        _context.SaveChanges();

        newReview.Request = request;

        return newReview.ToDto();
    }

    public ReviewRequestInfoDto CompleteReview(int requestId)
    {
        ReviewRequest request = _context.ReviewRequests.Single(r => r.Id == requestId);

        if (request.State == ReviewState.Requested)
            throw new RecademyException($"Completing review failed. Review request was not reviewed. Review request id: {request.Id}");

        request.State = ReviewState.Completed;
        _context.Update(request);
        _context.SaveChanges();

        return request.ToDto();
    }

    public ReviewRequestInfoDto AbandonReview(int requestId)
    {
        ReviewRequest request = _context.ReviewRequests.Single(r => r.Id == requestId);

        //TODO: check state
        request.State = ReviewState.Abandoned;
        _context.Update(request);
        _context.SaveChanges();

        return request.ToDto();
    }
}