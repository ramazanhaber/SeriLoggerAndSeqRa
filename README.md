
github link : https://github.com/ramazanhaber/SeriLoggerAndSeqRa

exe indirme linki: 
https://datalust.co/download

1- kurulacaklar
Install-Package Serilog.AspNetCore
Install-Package Serilog.Sinks.Seq
Install-Package Serilog -Version
Install-Package Serilog.Sinks.File
Install-Package Serilog.Sinks.Console
Install-Package Serilog.Settings.Configuration 
Install-Package Serilog.Extensions.Hosting

appsettings.json

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Serilog": {
    "Using": [ "Serilog.Sinks.Console", "Serilog.Sinks.File" ],
    "MinimumLevel": "Debug",
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "File",
        "Args": {
          "path": "Logs/applog-.txt",
          "rollingInterval": "Day"
        }
      },
      {
        "Name": "Seq",
        "ApiKey": "aP3718FM50jk5PSxKcec1",
        "Args": {
          "serverUrl": "http://localhost:5341" // Seq'in URL'si
        }
      }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName" ],
    "Properties": {
      "ApplicationName": "Your ASP.NET Core App"
    }
  },
  "AllowedHosts": "*"
}




Program.cs

using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Host.UseSerilog(); // Serilog'u kullanmak için yapılandırma


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSerilogRequestLogging();

app.UseCors(builder => builder
.AllowAnyHeader()
.AllowAnyMethod()
.SetIsOriginAllowed((host) => true)
.AllowCredentials()
);
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "myapi v1");
    });
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();




Controller.cs

  private readonly ILogger<WeatherForecastController> _logger;
  public WeatherForecastController(ILogger<WeatherForecastController> logger)
  {
      _logger = logger;
      _logger.LogInformation("rambo1");
      _logger.LogInformation("The current time is: {CurrentTime}", DateTimeOffset.UtcNow);
      _logger.LogInformation("rambo2");
  }

