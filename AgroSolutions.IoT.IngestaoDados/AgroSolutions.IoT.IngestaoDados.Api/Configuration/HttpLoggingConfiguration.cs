using Microsoft.AspNetCore.HttpLogging;

namespace AgroSolutions.IoT.IngestaoDados.Api.Configuration;

public static class HttpLoggingConfiguration
{
    public static void AddCustomHttpLogging(this IServiceCollection services)
    {
        services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
            logging.MediaTypeOptions.AddText("application/javascript");
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
            logging.CombineLogs = true;
        });
    }
}
