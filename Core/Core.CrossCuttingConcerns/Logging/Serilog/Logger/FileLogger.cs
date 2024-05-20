using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Logging.Serilog.Messages;
using Microsoft.Extensions.Configuration;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Logger;

public class FileLogger: LoggerServiceBase
{
    private IConfiguration _configuration;

    public FileLogger(IConfiguration configuration)
    {
        _configuration = configuration;
        
        FileLogConfiguration logConfiguration = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration").Get<FileLogConfiguration>() ?? throw new Exception(SerilogMessages.FileLogConfigurationNotFound);
    }
}