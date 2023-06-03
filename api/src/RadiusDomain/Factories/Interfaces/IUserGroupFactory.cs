using RadiusDomain.Entities;
using RadiusDomain.Exceptions;

namespace RadiusDomain.Factories.Interfaces;

public interface IUserGroupFactory
{
    /**
     * Create a new UserGroup.
     * <exception cref="EntityValidationException"></exception>
     * <returns>UserGroup created.</returns>
     */
    public UserGroup Create(string username, string group, int priority);
}