using Recademy.Shared.Dtos;

namespace Recademy.Api.Services.Abstraction
{
    public interface IReviewResponseService
    {
        ReviewResponseInfoDto SendReviewResponse(ReviewResponseCreateDto reviewResponseCreateDto);
    }
}