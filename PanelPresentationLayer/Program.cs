using PanelPresentationLayer.Infrastructure.JwtUtil;
using PanelPresentationLayer.Infrastructure.RazorUtils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(o=>o.Conventions.AddPageRoute("/Panel/Index", "")).AddRazorRuntimeCompilation();
builder.Services.AddScoped<IRenderViewToString, RenderViewToString>();
builder.Services.AddJwtAuthentication(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["code-token"]?.ToString();
    if (string.IsNullOrWhiteSpace(token) == false)
    {
        context.Request.Headers.Append("Authorization", $"Bearer {token}");
    }
    await next();
});
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 401)
    {
        context.Response.Redirect("/Authentication/Login");
    }
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();
app.Run();
