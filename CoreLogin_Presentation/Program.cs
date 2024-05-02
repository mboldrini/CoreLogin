using CoreLogin_Domain.Repositories;
using CoreLogin_Infrastructure.Data;
using CoreLogin_Infrastructure.Repository;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("connectionString");

// Configura��o do banco de dados
builder.Services.AddDbContext<DataContext>(options =>
{
  options.UseSqlServer(connectionString, b => b.MigrationsAssembly("CoreLogin_Infrastructure"));
});
builder.Services.AddScoped<DataContext, DataContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();

// Configura��o dos servi�os
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddResponseCompression(options =>
{
  options.Providers.Add<GzipCompressionProvider>();
  options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
});
builder.Services.AddResponseCaching();
builder.Services.AddCors();




// Configura��o do ambiente
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
  });
}

// Configura��o da pipeline de requisi��o HTTP
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();


app.UseRouting();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
  endpoints.MapControllers();
});

app.Run();
