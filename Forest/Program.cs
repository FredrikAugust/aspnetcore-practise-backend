using Forest.Data.Contexts;
using Forest.Data.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Auth0
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration.GetValue<string>("Auth0:Authority");
    options.Audience = builder.Configuration.GetValue<string>("Auth0:Audience");
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreateChallenge", policyBuilder => policyBuilder.RequireClaim("permissions", "create:challenge"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db Contexts
builder.Services.AddDbContext<ChallengesContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyHeader();
    policyBuilder.AllowAnyMethod();
    policyBuilder.AllowAnyOrigin();
});

// Seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var challengesContext = services.GetService<ChallengesContext>();
    challengesContext?.Database.EnsureCreated();
    ChallengeInitializer.Initialize(challengesContext);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();