using RadiusDomain.DTOs.User;

namespace RadiusDomain.UseCases.Interfaces;

public interface IUserUseCases
{
    /**
     * Pushes a list of users.
     * If the user exists, it is updated, if not, it is created.
     * <exception cref=""></exception>
     */
    public void Push(List<UserPushInDto> users);
}