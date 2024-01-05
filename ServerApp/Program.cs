using Microsoft.EntityFrameworkCore;
using ServerApp;
using ServerApp.Services;
using ServerApp.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("enigmatry_database"));
});
builder.Services.AddScoped<AddClientDataService>();
builder.Services.AddScoped<ClientInfoService>();
builder.Services.AddScoped<FinantialDocumentService>();
builder.Services.AddScoped<IfClientExists>(); 
builder.Services.AddScoped<IsProductSupportedService>();
builder.Services.AddScoped<IsTenantWhitelistedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
