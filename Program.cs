using KairosInstaller.Components;
using KairosInstaller.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure to listen on http://0.0.0.0:8080
builder.WebHost.UseUrls("http://0.0.0.0:8080");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register form schema service
builder.Services.AddSingleton<IFormSchemaService, FormSchemaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
