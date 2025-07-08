using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.DataAccess.Services;
using InterviewPass.DataAccess.UnitOfWork;
using InterviewPass.Infrastructure.Middlewares;
using InterviewPass.WebApi.Enums;
using InterviewPass.WebApi.Examples;
using InterviewPass.WebApi.Mapper;
using InterviewPass.WebApi.Models.Question;
using InterviewPass.WebApi.Models.User;
using InterviewPass.WebApi.Processors;
using JsonSubTypes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var encryptedSecret = builder.Configuration["JwtSettings:SecretKey"];
byte[] encryptedBytes = Convert.FromBase64String(encryptedSecret);

byte[] decryptedBytes = ProtectedData.Unprotect(
    encryptedBytes,
    null,
    DataProtectionScope.LocalMachine
);

string decryptedSecret = Encoding.UTF8.GetString(decryptedBytes);

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

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(decryptedSecret)),
            ClockSkew = TimeSpan.Zero
        };
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
builder.Services.AddSwaggerExamplesFromAssemblyOf<JobExampleDocumentation>();

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
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IExamProcessor, ExamProcessor>();
builder.Services.AddTransient<IJobProcessor, JobProcessor>();
builder.Services.AddTransient<JobSeekerRepository>();
builder.Services.AddTransient<HrRepository>();
//builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddTransient<Func<string, IUserRepository>>(serviceProvider => key =>
{
    return key switch
    {
        "JobSeeker" => serviceProvider.GetRequiredService<JobSeekerRepository>(),
        "Hr" => serviceProvider.GetRequiredService<HrRepository>(),
        _ => throw new KeyNotFoundException("Service not found.")
    };
});
builder.Services
    .AddCors(options =>
    {
        options.AddPolicy("AllowOrigin",
            builder => builder.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseCors("AllowOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandler>();
app.MapControllers();

app.Run();