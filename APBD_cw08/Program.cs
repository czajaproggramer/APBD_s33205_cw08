using APBD_cw08.DAK;
using APBD_cw08.Repos;
using APBD_cw08.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDbRepo, EFDbRepo>();
builder.Services.AddScoped<PatientsService>();
builder.Services.AddDbContext<UniversityTasksDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
builder.Services.AddOpenApi();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapControllers();

app.Run();
