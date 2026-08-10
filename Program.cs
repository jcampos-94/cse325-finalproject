using cse325_finalproject.Components;
using cse325_finalproject.Data;
using cse325_finalproject.Models;
using cse325_finalproject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Configure SQLite database storage using the application's connection string.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure ASP.NET Core Identity for user authentication and account management.
builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Redirect unauthenticated users to the login page.
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
});

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<CategoryService>();

builder.Services.AddScoped<ProductService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Apply pending database migrations when the application starts.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline and application middleware.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Handle login requests and create an authenticated user session.
app.MapPost("/account/login", async (
    HttpContext context,
    SignInManager<ApplicationUser> signInManager) =>
{
    var form = await context.Request.ReadFormAsync();

    var email = form["Email"].ToString();
    var password = form["Password"].ToString();

    var user = await signInManager.UserManager.FindByEmailAsync(email);

    if (user == null)
    {
        return Results.Redirect("/login?error=true");
    }

    var result = await signInManager.CheckPasswordSignInAsync(
        user,
        password,
        false);

    if (!result.Succeeded)
    {
        return Results.Redirect("/login?error=true");
    }

    await signInManager.SignInWithClaimsAsync(
        user,
        isPersistent: false,
        new[]
        {
        new System.Security.Claims.Claim(
            "DisplayName",
            user.DisplayName)
        });

    return Results.Redirect("/");
});

// Handle logout requests and end the user's authenticated session.
app.MapGet("/account/logout", async (
    SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();

    return Results.Redirect("/login");
});

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
