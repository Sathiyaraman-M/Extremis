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

    public async Task<IResult<UserInfoDto>> GetUserInfoAsync(string userId)
    {
        try
        {
            var appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null)
            {
                throw new Exception("User Not Found!");
            }
            var userInfo = new UserInfoDto()
            {
                UserName = appUser.UserName,
                FullName = appUser.FullName,
                DateOfBirth = appUser.DateOfBirth,
                City = appUser.City,
                Country = appUser.Country,
                SingleLineDescription = appUser.SingleLineDescription,
                Bio = appUser.Bio
            };
            return await Result<UserInfoDto>.SuccessAsync(userInfo);
        }
        catch (Exception e)
        {
            return await Result<UserInfoDto>.FailAsync(e.Message);
        }
    }

    public async Task<IResult> UpdateUserInfoAsync(UpdateUserInfoRequestDto request, string userId)
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