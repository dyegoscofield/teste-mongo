using TesteMongoDb.Api.Infrastructure;
using System.Reflection;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using StackSpot.Logging;
using StackSpot.Logging.Correlation;
using StackSpot.Logging.OpenTracing;
using System.IO.Compression;
using System.Text.Json.Serialization;
using TesteMongoDb.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogger(builder.Configuration)
                .WithOpenTracing()
                .WithCorrelation();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault);
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "mongo", Description = "Teste mongo DB", Version = "v1" });
});

builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.EnableForHttps = true;
});

builder.Services.AddInfrastructure();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

var app = builder.Build();

app.UsePathBase("/");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseResponseCompression();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health");
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Run();