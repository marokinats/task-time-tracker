using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using TaskTimeTrackerIdentity.Dal.Context;
using TaskTimeTrackerIdentity.Dal.Model.Identity;
using TaskTimeTrackerIdentity.Web.Areas.Identity;
using TaskTimeTrackerIdentity.Web.Services;
using TaskTimeTrackerIdentity.Web.Services.Interfaces;
using TaskTimeTrackerIdentity.Web.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Available by request
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<AppUser>>();
builder.Services.AddScoped<IMyTaskService, MyTaskService>();
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ISpaceService, SpaceService>();
builder.Services.AddScoped<IGroupService, GroupService>();


// Register AppState as a Singleton service to share value across the application
builder.Services.AddSingleton<AppState>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

