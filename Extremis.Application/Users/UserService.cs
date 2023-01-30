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

    public async Task<IResult<UserStatusDto>> GetUserStatusAsync(string userId)
    {
        try
        {
            var appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null)
            {
                throw new Exception("User Not Found!");
            }
            var userStatus = new UserStatusDto()
            {
                CustomStatus = appUser.CustomStatus,
                Status = appUser.Status
            };
            return await Result<UserStatusDto>.SuccessAsync(userStatus);
        }
        catch (Exception e)
        {
            return await Result<UserStatusDto>.FailAsync(e.Message);
        }
    }

    public async Task<IResult> UpdateUserStatusAsync(UpdateUserStatusRequestDto request, string userId)
    {
        try
        {
            if (userId != request.Id)
            {
                throw new Exception("User Not Found!");
            }
            var appUser = await _userManager.FindByIdAsync(request.Id);
            if (appUser == null)
            {
                throw new Exception("User Not Found!");
            }
            appUser.Status = request.Status;
            appUser.CustomStatus = request.CustomStatus;
            return await Result.SuccessAsync("Status updated successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> UpdateDeveloperInfoAsync(UpdateAppUserRequestDto request, string userId)
    {
        try
        {
            if (userId != request.Id)
            {
                throw new Exception("User Not Found!");
            }
            var appUser = await _userManager.FindByIdAsync(request.Id);
            if (appUser == null)
            {
                throw new Exception("User Not Found!");
            }
            appUser.UserName = request.UserName;
            appUser.FullName = request.FullName;
            appUser.DateOfBirth = request.DateOfBirth;
            appUser.City = request.City;
            appUser.Country = request.Country;
            appUser.SingleLineDescription = request.SingleLineDescription;
            appUser.Bio = request.Bio;
            await _userManager.UpdateAsync(appUser);
            return await Result.SuccessAsync("User successfully updated");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }
}