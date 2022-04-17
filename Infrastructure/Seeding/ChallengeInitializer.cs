using Domain.Entities;
using Infrastructure.Contexts;

namespace Infrastructure.Seeding;

public static class ChallengeInitializer
{
    public static void Initialize(ChallengesContext? challengesContext)
    {
        if (challengesContext == null) return;

        if (challengesContext.Challenges.Any()) return;

        var challenges = new List<Challenge>
        {
            new()
            {
                Name = "Beat SM 64 in 20 minutes",
                Description =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam lobortis nibh ut eros rutrum, nec egestas nunc viverra. Suspendisse pharetra dui in risus hendrerit, eu luctus neque vulputate. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Etiam et sem id sem elementum fringilla.",
                Points = 300,
                Answer = new Answer
                {
                    Value = "ctf{flag-1}"
                },
                Attachments =
                {
                    new ChallengeAttachment
                    {
                        Name = "hint.jpg",
                        Url = "http://127.0.0.1:10000/devstoreaccount1/challenge-attachments/klyseklingen.jpg",
                    },
                    new ChallengeAttachment
                    {
                        Name = "hint-2.jpg",
                        Url = "http://127.0.0.1:10000/devstoreaccount1/challenge-attachments/fredrik.jpg",
                    }
                }
            },
            new()
            {
                Name = "Beat 007 Goldeneye without taking damage",
                Description =
                    "Morbi laoreet nunc nibh, sit amet varius odio commodo in. Suspendisse posuere lectus sed est cursus, quis gravida dui maximus. In laoreet viverra tincidunt. Etiam tincidunt massa sed mi imperdiet fringilla. Integer ut libero eros. Curabitur vel ornare metus, non facilisis tortor.",
                Points = 500,
                Answer = new Answer
                {
                    Value = "ctf{flag-2}"
                }
            }
        };

        challengesContext.Challenges.AddRange(challenges);
        challengesContext.SaveChanges();
    }
}