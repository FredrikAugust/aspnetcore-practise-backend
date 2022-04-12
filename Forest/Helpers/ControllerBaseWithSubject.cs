using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Forest.Helpers;

public class ControllerBaseWithSubject : ControllerBase
{
    protected string? Subject => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}