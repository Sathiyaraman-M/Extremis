using Extremis.Repositories;
using Extremis.Wrapper;
using Microsoft.AspNetCore.Identity;

namespace Extremis.Users;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IResult> UpdateDeveloperInfo(UpdateAppUserDto infoDto, string userId)
    {
        try
        {
            if (userId != infoDto.Id)
            {
                throw new Exception("User Not Found!");
            }
            var appUser = await _userManager.FindByIdAsync(infoDto.Id);
            if (appUser == null)
            {
                throw new Exception("User Not Found!");
            }
            appUser.UserName = infoDto.UserName;
            appUser.FullName = infoDto.FullName;
            appUser.DateOfBirth = infoDto.DateOfBirth;
            appUser.City = infoDto.City;
            appUser.Country = infoDto.Country;
            appUser.SingleLineDescription = infoDto.SingleLineDescription;
            appUser.Bio = infoDto.Bio;
            await _userManager.UpdateAsync(appUser);
            return await Result.SuccessAsync("User successfully updated");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }
}