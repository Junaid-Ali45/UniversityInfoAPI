using UniversityInfoRepository.Interfaces;
using UniversityInfoService.Interfaces;
using UniversityInfoRepository.Repositories;
using UniversityInfoService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUniversityInfoRepository, UniversityInfoRepository.Repositories.UniversityInfoRepository>();
builder.Services.AddSingleton<IUniversityInfoService, UniversityInfoService.Services.UniversityInfoService>();
builder.Services.AddSingleton<IFileRepository, UniversityInfoRepository.Repositories.FileRepository>();
builder.Services.AddSingleton<IFileService, UniversityInfoService.Services.FileService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy"); // Add this line for CORS support

app.UseAuthorization();

app.MapControllers();

app.Run();
