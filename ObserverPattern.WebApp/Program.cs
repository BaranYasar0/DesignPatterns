using System.Reflection;
using BaseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ObserverPattern.WebApp.Models;
using ObserverPattern.WebApp.Observer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddSingleton<UserObserverSubject>(sp =>
{
    UserObserverSubject subject = new UserObserverSubject();
    subject.RegisterObserver(new UserObserverWriteToConsole(sp));
    subject.RegisterObserver(new UserObserverCreatedDiscount(sp));
    subject.RegisterObserver(new UserObserverSendEmail(sp));
    
    return subject;
});

builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

var app = builder.Build();

await using (var scope= app.Services.CreateAsyncScope())
{
    var db=scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

    var userManager=scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    db.Database.Migrate();

    if (!db.Users.Any())
    {
        await userManager.CreateAsync(new AppUser
        {
            UserName = "user1",
            Email = "user1@gmail.com",
        },"Password12*");

        await userManager.CreateAsync(new AppUser
        {
            UserName = "user2",
            Email = "user2@gmail.com",
        }, "Password12*");
        await userManager.CreateAsync(new AppUser
        {
            UserName = "user3",
            Email = "user3@gmail.com",
        }, "Password12*");
        await userManager.CreateAsync(new AppUser
        {
            UserName = "user4",
            Email = "user4@gmail.com",
        }, "Password12*");
        await userManager.CreateAsync(new AppUser
        {
            UserName = "user5",
            Email = "user5@gmail.com",
        }, "Password12*");
    }
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
