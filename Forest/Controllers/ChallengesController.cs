using Forest.Data.Contexts;
using Forest.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forest.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ChallengesController : ControllerBase
{
    private readonly ILogger<ChallengesController> _logger;

    private readonly ChallengesContext _challengesContext;

    public ChallengesController(ILogger<ChallengesController> logger, ChallengesContext challengesContext)
    {
        _logger = logger;
        _challengesContext = challengesContext;
    }

    [HttpGet]
    public async Task<IEnumerable<Challenge>> Get()
    {
        return await _challengesContext.Challenges.ToListAsync();
    }

    [HttpPost]
    [Authorize("CreateChallenge")]
    public async Task<ActionResult<Challenge>> Create(Challenge challenge)
    {
        _challengesContext.Challenges.Add(challenge);
        await _challengesContext.SaveChangesAsync();

        // Todo: Create endpoint to get _one_ challenge
        return CreatedAtAction("Get", challenge);
    }
}