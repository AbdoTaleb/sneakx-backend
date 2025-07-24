using Microsoft.EntityFrameworkCore;
using SneakX.API.Data;
using SneakX.API.Data.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SneakXContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// ✅ Use CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();



app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SneakXContext>();
    await ProductSeeder.SeedProductsAsync(context);
}

app.Run();
