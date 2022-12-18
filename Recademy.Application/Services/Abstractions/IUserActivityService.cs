using Recademy.Dto.Activity;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Recademy.Application.Services.Abstractions;

public interface IUserActivityService
{
    public Task<IReadOnlyCollection<UserActivityDto>> GetAllActivity();
    public Task<UserActivityDto> GetUserActivity(string userName);
}