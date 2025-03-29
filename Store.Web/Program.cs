using Microsoft.EntityFrameworkCore;
using Store;
using Store.Contractors;
using Store.Contractors.RoboKassa;
using Store.Data.EF;
using Store.Messages;
using Store.Web.Contractors;
using Store.Data.EF.Store;
using Store.Data.EF.Identity; 
using Store.Web.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession
    (options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(20);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    }
    );


//--- Регистрируем репозитории, бдшку и фабрику ----
builder.Services.AddEfRepositories(builder.Configuration.GetConnectionString("Store"));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Identity")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
    

builder.Services.AddSingleton<IDeliveryService, NovaPoshtaPostmateDeliveryService>();
builder.Services.AddSingleton<INotificationService, DebugNotificationService>();
builder.Services.AddSingleton<IPaymentService, CashPaymentService>();
builder.Services.AddSingleton<IPaymentService, RoboKassaPaymentService>();
builder.Services.AddSingleton<IWebContractorService, RoboKassaPaymentService>();
builder.Services.AddSingleton<ICommentRepository, CommentRepository>();
builder.Services.AddSingleton<ApplicationContextFactory>();
builder.Services.AddSingleton<CommentService>();
builder.Services.AddSingleton<BookService>();
builder.Services.AddSingleton<OrderService>();

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

app.UseSession();

app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
