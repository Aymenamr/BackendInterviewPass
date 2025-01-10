using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.DataAccess.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using JsonSubTypes;
using InterviewPass.WebApi.Models.User;
using InterviewPass.WebApi.Mapper;
using InterviewPass.WebApi.Enums;
using Swashbuckle.AspNetCore.Filters;
using InterviewPass.WebApi.Examples;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using InterviewPass.WebApi.Models.Question;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    //and define the device Discriminator
    options.SerializerSettings.Converters.Add(
        JsonSubtypesConverterBuilder
        .Of(typeof(UserModel), "UserType")
        .RegisterSubtype(typeof(UserJobSeekerModel), UserType.JobSeeker)
        .RegisterSubtype(typeof(UserHrModel), UserType.Hr)
        .SerializeDiscriminatorProperty()
        .Build());

}).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(
                JsonSubtypesConverterBuilder
                    .Of(typeof(QuestionModel), "QuestionType")
                    .RegisterSubtype(typeof(MultipleChoiceQuestionModel), "MultipleChoice")
                    .RegisterSubtype(typeof(TrueFalseQuestionModel), "TrueFalse")
                    .RegisterSubtype(typeof(PracticalQuestionModel), "Practical")
                    .RegisterSubtype(typeof(ObjectiveQuestionModel), "Objective")
                    .SerializeDiscriminatorProperty()
                    .Build());
        });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Automatically include XML comments from the assembly
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    // Enable support for examples
    options.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<UserExampleDocumentation>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ExamExampleDocumentation>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure Serilog
builder.Host.UseSerilog((context, loggerConfiguration) =>
{

    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration) // Read settings from appsettings.json
    .Enrich.FromLogContext();
});

//Dependency injection
builder.Services.AddTransient<DbContext, InterviewPassContext>();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<JobSeekerRepository>();
builder.Services.AddTransient<HrRepository>();
builder.Services.AddTransient<Func<string, IUserRepository>>(serviceProvider => key =>
{
    return key switch
    {
        "JobSeeker" => serviceProvider.GetRequiredService<JobSeekerRepository>(),
        "Hr" => serviceProvider.GetRequiredService<HrRepository>(),
        _ => throw new KeyNotFoundException("Service not found.")
    };
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
//builder.WebHost.UseUrls("http://*:1302");
app.UseSerilogRequestLogging();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
