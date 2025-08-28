using EmployeeAdminPortalLST.Data;
using EmployeeAdminPortalLST.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// define builder.Services
var Services = builder.Services;
//---------------------------------------
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
Services.AddControllers();
Services.AddEndpointsApiExplorer();
Services.AddSwaggerGen();
//Services.Configure.< DbContext > (
//    builder.Configuration.GetSection("DefaultSetting"));
//Services.AddSingleton<DbContextId>(s => s.GetRequiredKeyedService<IOptions<DbContext>>().Value);
Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));

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
