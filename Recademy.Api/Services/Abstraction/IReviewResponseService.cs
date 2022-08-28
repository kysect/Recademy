using Recademy.Shared.Dtos.Reviews;

namespace Recademy.Api.Services.Abstraction
{
    public interface IReviewResponseService
    {
        ReviewResponseInfoDto SendReviewResponse(ReviewResponseCreateDto reviewResponseCreateDto);
    }
}