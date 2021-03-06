using Domain.Dto;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Helpers;

namespace Web.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class AnswerController : ControllerBaseWithSubject
{
    private readonly ChallengesContext _challengesContext;
    private readonly ILogger<AnswerController> _logger;

    public AnswerController(ChallengesContext challengesContext, ILogger<AnswerController> logger)
    {
        _challengesContext = challengesContext;
        _logger = logger;
    }

    [HttpPost("{challengeId:int}")]
    public async Task<IActionResult> Answer([FromBody] string answer, int challengeId)
    {
        var challenge = await _challengesContext.Challenges.Include(challenge1 => challenge1.Answer)
            .FirstOrDefaultAsync(challenge1 => challenge1.ChallengeId == challengeId);

        if (challenge == null) return NotFound();

        if (answer != challenge.Answer.Value)
            return Ok(new AnswerDto
            {
                AnswerStatus = AnswerDto.Status.Incorrect
            });

        if (Subject == null)
        {
            _logger.LogWarning("Attempted to solve challenge without user subject on JWT token");
            return BadRequest("No subject on JWT token");
        }

        if (await _challengesContext.Solves.FirstOrDefaultAsync(solve =>
                solve.Subject == Subject && solve.ChallengeId == challengeId) != null)
        {
            return BadRequest("You have already solved this challenge");
        }

        var solve = new Solve
        {
            Subject = Subject,
            Timestamp = DateTime.Now,
            ChallengeId = challenge.ChallengeId
        };

        _challengesContext.Solves.Add(solve);
        await _challengesContext.SaveChangesAsync();

        return Ok(new AnswerDto
        {
            AnswerStatus = AnswerDto.Status.Correct
        });
    }
}