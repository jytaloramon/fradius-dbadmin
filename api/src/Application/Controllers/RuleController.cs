using FradminDomain.DTOs;
using FradminDomain.UseCases;
using FradminDomain.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("[controller]")]
public class RuleController : ControllerBase
{
    private readonly IRuleUseCase _useCase;

    private readonly ILogger<RuleController> _logger;

    public RuleController(ILogger<RuleController> logger)
    {
        _useCase = new RuleUseCase();
        _logger = logger;
    }

    [HttpGet("/rules")]
    [ProducesResponseType(typeof(RuleGroupDto[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRules()
    {
        var rules = await _useCase.GetAllGroups();

        return new OkObjectResult(rules);
    }
}