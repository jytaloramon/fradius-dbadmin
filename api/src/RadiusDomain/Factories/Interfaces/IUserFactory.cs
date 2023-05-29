using RadiusDomain.Entities;
using RadiusDomain.Exceptions;

namespace RadiusDomain.Factories.Interfaces;

public interface IUserFactory
{   
    /**
     * Create a new User.
     * <exception cref="EntityValidationException"></exception>
     * <returns>User created.</returns>
     */
    public User Create(string username, List<RadiusAttribute> attributes, List<UserGroup> groups);
}