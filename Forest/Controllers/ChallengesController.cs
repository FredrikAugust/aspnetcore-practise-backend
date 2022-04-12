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

    [HttpGet(Name = "GetAll")]
    public async Task<IEnumerable<Challenge>> Get()
    {
        return await _challengesContext.Challenges.ToListAsync();
    }

    [HttpGet("{id:int}", Name = "GetChallenge")]
    public async Task<ActionResult<Challenge>> Get(int id)
    {
        var challenge = await _challengesContext.Challenges.Include(challenge1 => challenge1.Attachments)
            .FirstOrDefaultAsync(challenge1 => challenge1.ChallengeId == id);

        if (challenge == null) return NotFound();

        return Ok(challenge);
    }

    [HttpPost]
    [Authorize("CreateChallenge")]
    public async Task<ActionResult<Challenge>> Create(Challenge challenge)
    {
        _challengesContext.Challenges.Add(challenge);
        await _challengesContext.SaveChangesAsync();

        // Todo: Create endpoint to get _one_ challenge
        return CreatedAtAction("Get", new {id = challenge.ChallengeId}, challenge);
    }

    [HttpDelete]
    [Authorize("DeleteChallenge")]
    public async Task<ActionResult> Delete(int challengeId)
    {
        var challenge = await _challengesContext.Challenges.FindAsync(challengeId);

        if (challenge == null) return NotFound();

        _challengesContext.Challenges.Remove(challenge);

        await _challengesContext.SaveChangesAsync();

        return Ok();
    }
}