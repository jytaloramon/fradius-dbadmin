using FradminDomain.DTOs;
using FradminDomain.Entities.Validations;
using FradminDomain.Factories;
using FradminDomain.Repositories;
using FradminDomain.SGBDs;
using FradminDomain.UseCases;
using FradminDomain.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController
{
    private readonly IAdminUseCase _useCase;

    private readonly ILogger<AdminGroupController> _logger;

    public AdminController(SgbdBase db, ILogger<AdminGroupController> logger)
    {
        _useCase = new AdminUseCase(new AdminFactory(new AdminValidation()), new AdminRepository(db));
        _logger = logger;
    }

    [HttpGet("/admins")]
    [ProducesResponseType(typeof(List<AdminFullDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAdmins()
    {
        var admins = await _useCase.GetAll();

        return new OkObjectResult(admins);
    }

    [HttpPost("/admins")]
    [ProducesResponseType(typeof(AdminFullDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddAdmin(AdminAddDto adminGroupNewDto)
    {
        var createdAdminGroup = await _useCase.Add(adminGroupNewDto);

        return new OkObjectResult(createdAdminGroup);
    }
}