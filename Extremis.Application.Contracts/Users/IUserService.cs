using Extremis.Wrapper;

namespace Extremis.Users;

public interface IUserService
{
    Task<IResult<UserStatusDto>> GetUserStatusAsync(string userId);
    Task<IResult> UpdateUserStatusAsync(UpdateUserStatusRequestDto request, string userId);
    Task<IResult> UpdateDeveloperInfoAsync(UpdateAppUserRequestDto request, string userId);
}