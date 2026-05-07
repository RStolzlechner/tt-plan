
using System.Text.Json.Serialization;
using Microsoft.OpenApi;
using TtPlan.WebApi.Configuration;
using TtPlan.WebApi.Configuration.Modules;
using TtPlan.WebApi.Configuration.Options;
using TtPlan.WebApi.Repositories.JournalRepo;
using TtPlan.WebApi.Repositories.MilestoneRepo;
using TtPlan.WebApi.Repositories.ProjectRepo;
using TtPlan.WebApi.Repositories.TaskRepo;

var builder = WebApplication.CreateBuilder(args);

//api and cors
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://test.tauerntec.at").AllowAnyHeader().AllowAnyMethod();
    });
});

//options
builder.Services.Configure<PostgresOptions>(builder.Configuration.GetSection(PostgresOptions.Position));

//database
var pgOptions = builder.Configuration.GetSection(PostgresOptions.Position).Get<PostgresOptions>();
if (pgOptions is null)
    throw new ArgumentException("no postgres options provided.");
builder.Services.AddDatabase(pgOptions.ConnectionString);

//repos
builder.Services.AddScoped<IJournalRepository, JournalRepository>();
builder.Services.AddScoped<IMilestoneRepository, MilestoneRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

//services

//controller
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

//swagger
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                          Enter 'Bearer' [space] and then your token in the text input below.
                          \r\n\r\nExample: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecuritySchemeReference("Bearer", document), []
            }
        });
    });
}

//build
var app = builder.Build();

//swagger
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//https and cors
app.UseHttpsRedirection();
app.UseCors("Frontend");

//controller
app.MapControllers();

//run it
app.Run();