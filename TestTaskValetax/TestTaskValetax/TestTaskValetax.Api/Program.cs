using TestTaskValetax.Api.Middleware;
using TestTaskValetax.Core.Options;
using TestTaskValetax.Infrastructure;
using TestTaskValetax.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<OptionsDatabase>(builder.Configuration.GetSection(OptionsDatabase.REGION));

builder.Services.AddTransient<BufferingMiddleware>();
builder.Services.AddTransient<HttpContextDataMiddleware>();
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddScoped<HttpContextData>();

builder.Services.AddPresentation();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseMiddleware<BufferingMiddleware>();
app.UseMiddleware<HttpContextDataMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });

//app.UseAuthorization();

app.MapControllers();

app.Run();
