using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Helpers;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ScoreController : ControllerBaseWithSubject
{
    private readonly ChallengesContext _challengesContext;

    /**
     * TODO: Rewrite models and folders, clean up front- and backend to make it less cluttered
     */

    public ScoreController(ChallengesContext challengesContext)
    {
        _challengesContext = challengesContext;
    }

    [HttpGet]
    public async Task<ActionResult> GetScore()
    {
        if (Subject == null) return BadRequest("No subject in JWT token");

        var solves = await _challengesContext.Solves.Where(solve => solve.Subject == Subject).ToListAsync();
        var challenges = await _challengesContext.Challenges.ToListAsync();

        var solvedChallenges = challenges.IntersectBy(solves.Select(solve => solve.ChallengeId), challenge => challenge.ChallengeId);

        return Ok(solvedChallenges.Select(challenge => challenge.Points).Sum());
    }
}