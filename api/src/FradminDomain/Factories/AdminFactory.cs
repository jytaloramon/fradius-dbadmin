using FluentValidation;
using FradminDomain.Entities;
using FradminDomain.Factories.Interfaces;

namespace FradminDomain.Factories;

public class AdminFactory : BaseFactory<Admin>, IAdminFactory
{
    public AdminFactory(AbstractValidator<Admin> validator) : base(validator)
    {
    }

    public Admin Create(string username, string email, string password, AdminGroup group, bool isActive)
    {
        var admin = new Admin
            { Username = username, Email = email, Password = password, Group = group, IsActive = isActive };

        var result = RunValidator(admin, "Username", "Email", "Password");

        if (result.IsValid) return admin;

        throw CreateEntityValidationException(result.Errors);
    }
}