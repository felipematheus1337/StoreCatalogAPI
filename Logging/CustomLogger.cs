
namespace StoreCatalogAPI.Logging;

public class CustomLogger : ILogger
{

    readonly string loggerName;
    readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomLogger(string loggerName, CustomLoggerProviderConfiguration loggerConfig)
    {
        this.loggerName = loggerName;
        this.loggerConfig = loggerConfig;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string mensagem = $"{logLevel.ToString()} : {eventId} - {formatter(state, exception)}"
        EscreverTextoNoArquivo(mensagem);
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
        string caminhoArquivo = this.GetLoggerFolderPath();

        using (StreamWriter streamWriter = new StreamWriter(caminhoArquivo, true))
        {
            try
            {
                streamWriter.WriteLine(mensagem);
                streamWriter.Close();
            } catch(Exception)
            {
                throw;
            }
        }
    }

    private string GetLoggerFolderPath()
    {
        string logFolderPath = Directory.GetCurrentDirectory() + "logginFiles";

        if (!Directory.Exists(logFolderPath)) Directory.CreateDirectory(logFolderPath);

        return logFolderPath;

    }
}
