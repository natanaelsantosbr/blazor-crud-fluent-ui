using Microsoft.FluentUI.AspNetCore.Components;
using BlzFluentUICrud.Components;
using BlzFluentUICrud.Context;
using Microsoft.EntityFrameworkCore;
using BlzFluentUICrud.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddFluentUIComponents();

var connectionString = builder.Configuration.GetConnectionString("Sqllite");
builder.Services.AddDbContext<AppDbContext>(a=> a.UseSqlite(connectionString));

builder.Services.AddScoped<IAlunoService, AlunoService>();

var app = builder.Build();

CreateDatabase(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

static void CreateDatabase(WebApplication app)
{
    var serviceScope = app.Services.CreateScope();
    var dataContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
    dataContext.Database.EnsureCreated();
}
