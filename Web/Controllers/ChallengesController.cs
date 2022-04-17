using Domain.BindingModels;
using Domain.Entities;
using Domain.ViewModels;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Helpers;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ChallengesController : ControllerBaseWithSubject
{
    private readonly ILogger<ChallengesController> _logger;

    private readonly ChallengesContext _challengesContext;

    public ChallengesController(ILogger<ChallengesController> logger, ChallengesContext challengesContext)
    {
        _logger = logger;
        _challengesContext = challengesContext;
    }

    [HttpGet(Name = "GetAll")]
    public async Task<ActionResult> Get()
    {
        if (Subject == null) return BadRequest("Subject not present in JWT token");
        
        var userSolves = await _challengesContext.Solves.Where(solve => solve.Subject == Subject).ToListAsync();
        var challenges = await _challengesContext.Challenges.ToListAsync();
        
        return Ok(challenges.Select(challenge => new ChallengeViewModel
        {
            Challenge = challenge,
            Solved = userSolves.Any(solve => solve.ChallengeId == challenge.ChallengeId)
        }));
    }

    [HttpGet("{challengeId:int}", Name = "GetChallenge")]
    public async Task<ActionResult<Challenge>> Get(int challengeId)
    {
        var challenge = await _challengesContext.Challenges.Include(challenge1 => challenge1.Attachments)
            .AsNoTracking().FirstOrDefaultAsync(challenge1 => challenge1.ChallengeId == challengeId);

        if (challenge == null) return NotFound();

        if (Subject == null) return BadRequest("Subject is not present on JWT token");

        var solved =
            await _challengesContext.Solves.FirstOrDefaultAsync(solve => solve.Subject == Subject && solve.ChallengeId == challengeId);

        if (solved == null)
        {
            return Ok(new ChallengeViewModel
            {
                Solved = false,
                Challenge = challenge
            });
        }

        var answer = await _challengesContext.Answers.FirstOrDefaultAsync(answer => answer.ChallengeId == challengeId);

        return Ok(new ChallengeViewModel
        {
            Solved = true,
            Challenge = challenge,
            Answer = answer
        });
    }

    [HttpPost]
    [Authorize("CreateChallenge")]
    public async Task<ActionResult<Challenge>> Create([FromBody] CreateChallengeBindingModel challengeBindingModel)
    {
        var challenge = new Challenge
        {
            Name = challengeBindingModel.Name,
            Description = challengeBindingModel.Description,
            Points = challengeBindingModel.Points,
            Answer = new Answer
            {
                Value = challengeBindingModel.Answer
            }
        };

        _challengesContext.Challenges.Add(challenge);
        await _challengesContext.SaveChangesAsync();

        return CreatedAtAction("Get", new {id = challenge.ChallengeId}, challenge);
    }

    [HttpDelete("{challengeId:int}")]
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