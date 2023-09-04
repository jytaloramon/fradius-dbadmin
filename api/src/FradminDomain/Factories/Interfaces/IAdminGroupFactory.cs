using FradminDomain.Entities;
using FradminDomain.Exceptions;
using FradminDomain.ValueObjects;

namespace FradminDomain.Factories.Interfaces;

public interface IAdminGroupFactory
{
    /**
     * Create a new AdminGroup.
     * <exception cref="EntityValidationException"></exception>
     * <returns>Group Created.</returns>
     */
    public AdminGroup Create(int id);
    
    /**
     * Create a new AdminGroup.
     * <exception cref="EntityValidationException"></exception>
     * <returns>Group Created.</returns>
     */
    public AdminGroup Create(string name, HashSet<Rules> rules);
}