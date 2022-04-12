using System.Text.Json.Serialization;
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
    options.AddPolicy("CreateChallenge",
        policyBuilder => policyBuilder.RequireClaim("permissions", "create:challenge"));
    options.AddPolicy("DeleteChallenge",
        policyBuilder => policyBuilder.RequireClaim("permissions", "delete:challenge"));
});

// Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowAnyOrigin();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db Contexts
builder.Services.AddDbContext<ChallengesContext>();

builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors();

// Seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var challengesContext = services.GetService<ChallengesContext>();
    challengesContext?.Database.EnsureCreated();
    ChallengeInitializer.Initialize(challengesContext);
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();