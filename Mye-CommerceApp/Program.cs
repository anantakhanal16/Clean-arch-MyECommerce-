using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Infra.Dependencies;
using Infrastructure.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.InfraServices(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseMiddleware<ExceptionHandlingMiddlewares>();


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
        endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {

        var context = services.GetRequiredService<ApplicationDbContext>();
        
        var Identitycontext = services.GetRequiredService<AppIdentityDbcontext>();
        
        Identitycontext.Database.Migrate();

        context.Database.Migrate();
    
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while applying migrations or creating the database.");
    }
}

app.Run();
