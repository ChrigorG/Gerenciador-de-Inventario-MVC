using Application.Services;
using Data.Context;
using InfrastructureIoC;
using Microsoft.AspNetCore.Authentication.Cookies;
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

// Configurando a autenticação com cookies, caso não estiver autenticado, será direcionado para a Login
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.ExpireTimeSpan = TimeSpan.FromDays(1); // Tempo de expiração do cookie
    });

// Adicionar serviços ao contêiner (Injeção de Dependência)
builder.Services.AddInfrastructureIoC();

// Adicionando o serviço de sessão
builder.Services.AddDistributedMemoryCache(); // Usa a memória para armazenar os dados da sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);  // Tempo de expiração da sessão
    options.Cookie.HttpOnly = true;  // A sessão será acessível apenas via HTTP
    options.Cookie.IsEssential = true; 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Usando a sessão no pipeline de requisições
app.UseSession();  // Isso permite o uso da sessão na aplicação
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
