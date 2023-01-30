using Extremis.Wrapper;

namespace Extremis.Users;

public interface IUserService
{
    Task<IResult> UpdateDeveloperInfo(UpdateAppUserDto infoDto, string userId);
}