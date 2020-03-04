using Recademy.Library.Dto;

namespace Recademy.Api.Services.Abstraction
{
    public interface IReviewResponseService
    {
        ReviewResponseInfoDto SendReviewResponse(ReviewResponseCreateDto reviewResponseCreateDto);
    }
}