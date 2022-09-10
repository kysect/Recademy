using Microsoft.EntityFrameworkCore;

using Recademy.Application.Mappings;
using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Reviews;
using Recademy.Core.Types;
using Recademy.DataAccess;
using Recademy.Dto.Reviews;
using System;
using System.Linq;

namespace Recademy.Application.Services.Implementations;

public class ReviewResponseService : IReviewResponseService
{
    private readonly RecademyContext _context;

    public ReviewResponseService(RecademyContext context)
    {
        _context = context;
    }

    public ReviewResponseInfoDto SendReviewResponse(ReviewResponseCreateDto reviewResponseCreateDto)
    {
        ArgumentNullException.ThrowIfNull(reviewResponseCreateDto);

        ReviewRequest request = _context
            .ReviewRequests
            .Include(s => s.ProjectInfo)
            .ThenInclude(p => p.Skills)
            .Include(s => s.User)
            .FirstOrDefault(r => r.Id == reviewResponseCreateDto.ReviewRequestId);

        if (request == null)
            throw RecademyException.ReviewRequestNotFound(reviewResponseCreateDto.ReviewRequestId);

        if (request.State == ProjectState.Completed || request.State == ProjectState.Abandoned)
            throw new RecademyException("Failed to send review. It is already closed.");

        ReviewResponse newReview = reviewResponseCreateDto.FromDto();

        request.State = ProjectState.Reviewed;

        _context.ReviewResponses.Add(newReview);
        _context.SaveChanges();

        newReview.ReviewRequest = request;

        return newReview.ToDto();
    }
}