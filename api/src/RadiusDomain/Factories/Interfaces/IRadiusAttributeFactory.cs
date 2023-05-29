using RadiusDomain.Entities;
using RadiusDomain.Exceptions;

namespace RadiusDomain.Factories.Interfaces;

public interface IRadiusAttributeFactory
{
    /**
     * Create a new RadiusAttribute.
     * <exception cref="EntityValidationException"></exception>
     * <returns>RadiusAttribute created.</returns>
     */
    public RadiusAttribute Create(string name, string op, string value);
}