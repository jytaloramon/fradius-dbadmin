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

    [HttpGet("/groups/{id}")]
    [ProducesResponseType(typeof(List<AdminGroupFullDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAdminGroup(int id)
    {
        var group = await _useCase.GetById(id);

        return group != null
            ? new OkObjectResult(group)
            : new NotFoundObjectResult(new { msg = $"ID ({id}) not found" });
    }

    [HttpGet("/groups")]
    [ProducesResponseType(typeof(List<AdminGroupFullDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAdminGroups()
    {
        var groups = await _useCase.GetAll();

        return new OkObjectResult(groups);
    }

    [HttpPost("/groups")]
    [ProducesResponseType(typeof(AdminGroupFullDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddAdminGroup(AdminGroupAddDto adminGroupDto)
    {
        var createdAdminGroup = await _useCase.Add(adminGroupDto);

        return new OkObjectResult(createdAdminGroup);
    }
}