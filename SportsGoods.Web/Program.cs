using Microsoft.EntityFrameworkCore;
using SportsGoods.App.Services;
using SportsGoods.Core.Models;
using SportsGoods.Data.DAL;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Administrator>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddMvc();

builder.Services.AddTransient<ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();


using (var scope = app.Services.CreateScope())
{
    var productService = scope.ServiceProvider.GetRequiredService<ProductService>();
    var assemblyLocation = Assembly.GetExecutingAssembly().Location;
    var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
    var solutionDirectory = Path.Combine(assemblyDirectory, "..", "..", "..", "..");
    var testDataDirectory = Path.Combine(solutionDirectory, "SolutionItems");
    var productsXmlPath = Path.Combine(testDataDirectory, "products.xml");
    await productService.SeedProductsFromXmlAsync(productsXmlPath);
}

app.Run();
