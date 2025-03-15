using Microsoft.EntityFrameworkCore;
using Project2.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<PePrn222TrialContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DB"))
);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
