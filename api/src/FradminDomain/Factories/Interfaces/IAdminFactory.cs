using FradminDomain.Entities;

namespace FradminDomain.Factories.Interfaces;

public interface IAdminFactory
{
    /**
     * Create a new Admin.
     * <exception cref="EntityValidationException"></exception>
     * <returns>Admin Created.</returns>
     */
    public Admin Create(string username, string email, string password, AdminGroup group, bool isActive);
}