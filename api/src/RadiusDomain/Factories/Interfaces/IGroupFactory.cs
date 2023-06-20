using RadiusDomain.Entities;
using RadiusDomain.Exceptions;

namespace RadiusDomain.Factories.Interfaces;

public interface IGroupFactory
{
    /**
     * Create a new Group.
     * <exception cref="EntityValidationException"></exception>
     * <returns>Group Created.</returns>
     */
    public Group Create(string name);

    /**
     * Create a new Group.
     * <exception cref="EntityValidationException"></exception>
     * <returns>Group Created.</returns>
     */
    public Group Create(string name, List<RadiusAttribute> attributes);
}