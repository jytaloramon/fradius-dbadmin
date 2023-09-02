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
public class AdminGroupController : ControllerBase
{
    private readonly IAdminGroupUseCase _useCase;

    private readonly ILogger<AdminGroupController> _logger;

    public AdminGroupController(SgbdBase bd, ILogger<AdminGroupController> logger)
    {
        _useCase = new AdminGroupUseCase(
            new AdminGroupFactory(new AdminGroupValidation()),
            new AdminGroupRepository(bd));

        _logger = logger;
    }

    [HttpGet("/{id:int:range(1,32767)}")]
    [ProducesResponseType(typeof(List<AdminGroupFullDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAdminGroup(short id)
    {
        var group = await _useCase.GetById(id);

        return group != null ? new OkObjectResult(group) : new NotFoundObjectResult(new { msg = $"ID ({id}) not found" });
    }

    [HttpGet("/")]
    [ProducesResponseType(typeof(List<AdminGroupFullDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAdminGroups()
    {
        var groups = await _useCase.GetAll();

        return new OkObjectResult(groups);
    }

    [HttpPost("/")]
    [ProducesResponseType(typeof(AdminGroupFullDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(AdminGroupNewDto adminGroupNewDto)
    {
        var createdAdminGroup = await _useCase.Add(adminGroupNewDto);

        return new OkObjectResult(createdAdminGroup);
    }
}