using ExpenseTracker.Data.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddCors(setup =>
{
	setup.AddPolicy("DevPolicy", policy =>
	{
		policy.AllowAnyHeader().AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
	});
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseCors("DevPolicy");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
