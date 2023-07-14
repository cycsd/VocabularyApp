using DataLayer.EFCode;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.DictionaryService;
using ServiceLayer.DictionaryService.Concrete;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var defaultConnection = "DefaultConnection";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<VocabularyAppContext>(
    options => options.UseSqlServer(config.GetConnectionString(defaultConnection)));
builder.Services.AddScoped<ICoreDictionaryService, DictionaryService>();
builder.Services.AddScoped<TagService>();

var MyLocalCorsPolicy = "LocalCors";
var Local = "http://127.0.0.1:3000";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyLocalCorsPolicy,
        policy =>
        {
            policy.WithOrigins(Local)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
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
app.UseCors(MyLocalCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
