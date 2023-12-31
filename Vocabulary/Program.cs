using DataLayer.EFCode;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.DictionaryService;
using ServiceLayer.DictionaryService.Concrete;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<VocabularyAppContext>(
    options => options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
    );
builder.Services.AddScoped<ICoreDictionaryService, DictionaryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
