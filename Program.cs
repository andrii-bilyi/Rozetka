using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Rozetka.Data;
using Rozetka.Middleware;
using Rozetka.Services;
using Rozetka.Services.Hash;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60); // время ожидания
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Register DbContext and services
//builder.Services.AddDbContext<DataContext>(options =>
//    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
//        new MySqlServerVersion(new Version(8, 0, 21)),
//        mySqlOptions => mySqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSingleton<IHashService, Md5HashService>();


String? connectionString =
    builder
    .Configuration
    .GetConnectionString("DefaultConnection");
MySqlConnection connection = new(connectionString);

//builder.Services.AddDbContext<DataContext>(options =>
//    options.UseMySql(
//        connection,
//        ServerVersion.AutoDetect(connection),
//        serverOptions =>
//            serverOptions
//            .MigrationsHistoryTable(
//                tableName: HistoryRepository.DefaultTableName,
//                schema: "Rozetka")
//            .SchemaBehavior(
//                MySqlSchemaBehavior.Translate,
//                (schema, table) => $"{schema}_{table}")
//    )
//);

// Register DbContext and services
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDistributedMemoryCache();


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

app.UseMiddleware<AuthSessionMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Initialize database
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var context = services.GetRequiredService<DataContext>();
//    //DbInitializer.Initialize(context);
//}

app.Run();


// привіт !!!