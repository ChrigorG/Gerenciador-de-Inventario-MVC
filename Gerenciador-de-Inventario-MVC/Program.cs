using Application.Services;
using Data.Context;
using InfrastructureIoC;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurando o DbContext com Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Data"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adicionar serviços ao contêiner (Injeção de Dependência)
builder.Services.AddInfrastructureIoC();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Aplicando a migrations automaticamente
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();

    var initDbService = serviceProvider.GetRequiredService<InitDbService>();
    initDbService.InitDb();
}

app.Run();
