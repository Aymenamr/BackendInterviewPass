using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.DataAccess.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using JsonSubTypes;
using InterviewPass.WebApi.Models.User;
using InterviewPass.WebApi.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
//and define the device Discriminator
options.SerializerSettings.Converters.Add(
    JsonSubtypesConverterBuilder
    .Of(typeof(UserModel), "UserType")
    .RegisterSubtype(typeof(UserJobSeekerModel), "JobSeeker")
    .RegisterSubtype(typeof(UserHrModel), "Hr")
    .SerializeDiscriminatorProperty()
    .Build());

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Automatically include XML comments from the assembly
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

builder.Services.AddAutoMapper(typeof(MappingProfile));
//Dependency injection
builder.Services.AddTransient<DbContext, InterviewPassContext>();
builder.Services.AddTransient<IExamRepository, ExamRepository>();
builder.Services.AddTransient<IJobSeekerRepository, JobSeekerRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
