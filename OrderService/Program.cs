using Microsoft.AspNetCore.Hosting;
using Serilog;
using SharedModel.Extensions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
try
{
    Log.Information("Starting web host");
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();

    builder.Host.UseSerilog((ctx, lc) => lc
         .WriteTo.Console());


    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddSwaggerGen(options =>
    {
        options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    });
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseMiddleware<ExceptionMiddleware>();
    app.UseMiddleware<RequestResponseHeadersMiddleware>();

    app.MapControllers();



    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
