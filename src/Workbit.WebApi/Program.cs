using Workbit.Infrastructure.Extensions;
using Workbit.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddApplicationDbContext(builder.Configuration);

builder.Services.AddApplicationIdentity(builder.Configuration);


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

await app.SeedRolesAsync();

app.MapControllers();

app.Run();
