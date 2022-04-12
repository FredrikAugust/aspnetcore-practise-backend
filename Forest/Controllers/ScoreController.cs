using Forest.Data.Contexts;
using Forest.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forest.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ScoreController : ControllerBaseWithSubject
{
    private readonly ChallengesContext _challengesContext;


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