using Recademy.Dto.Reviews;

namespace Recademy.Application.Services.Abstractions
{
    public interface IReviewResponseService
    {
        ReviewResponseInfoDto SendReviewResponse(ReviewResponseCreateDto reviewResponseCreateDto);
    }
}