using Microsoft.AspNetCore.Mvc;
using RadiusDomain.DTOs.User;
using RadiusDomain.UseCases.Interfaces;

namespace Application.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserUseCases _useCases;

    private readonly ILogger<UserController> _logger;

    public UserController(IUserUseCases useCases, ILogger<UserController> logger)
    {
        _useCases = useCases;
        _logger = logger;
    }

    [HttpPut("/users")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult PushUsers(List<UserPushInDto> users)
    {
        return new NoContentResult();
    }
}