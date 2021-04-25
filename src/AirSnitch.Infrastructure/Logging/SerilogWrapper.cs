using System;
using Serilog;
using Serilog.Core;

namespace AirSnitch.Core.Infrastructure.Logging
{
    public class SerilogWrapper : ILog
    {
        private static readonly Logger _logger;

        static SerilogWrapper()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();  
        }
        
        ///<inheritdoc/>
        public void Trace(object message)
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc/>
        public void Trace(object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc/>
        public void TraceFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc/>
        public void TraceFormat(string format, Exception exception, params object[] args)
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc/>
        public void Debug(object message)
        {
            _logger.Debug($"{@message}");
        }

        ///<inheritdoc/>
        public void Debug(object message, Exception exception)
        {
            _logger.Debug($"Message: @{message}, Exception : @{exception}");
        }

        ///<inheritdoc/>
        public void DebugFormat(string format, params object[] args)
        {
            _logger.Debug(format, args);
        }

        ///<inheritdoc/>
        public void Info(object message)
        {
            _logger.Information($"{@message}");
        }
        
        ///<inheritdoc/>
        public void Info(object message, Exception exception)
        {
            _logger.Information($"Message: @{message}, Exception : @{exception}");
        }
        
        ///<inheritdoc/>
        public void InfoFormat(string format, params object[] args)
        {
            _logger.Information(format, args);
        }
        
        ///<inheritdoc/>
        public void Warn(object message)
        {
            _logger.Warning($"{@message}");
        }

        ///<inheritdoc/>
        public void Warn(object message, Exception exception)
        {
           _logger.Warning($"Message: @{message}, Exception : @{exception}");
        }

        ///<inheritdoc/>
        public void WarnFormat(string format, params object[] args)
        {
            _logger.Warning(format, args);
        }

        ///<inheritdoc/>
        public void Error(object message)
        {
            _logger.Error($"{@message}");
        }

        ///<inheritdoc/>
        public void Error(object message, Exception exception)
        {
            _logger.Error($"Message: @{message}, Exception : @{exception}");
        }

        ///<inheritdoc/>
        public void ErrorFormat(string format, params object[] args)
        {
            _logger.Error(format, args);
        }

        ///<inheritdoc/>
        public void Fatal(object message)
        {
            _logger.Fatal($"{@message}");
        }

        ///<inheritdoc/>
        public void Fatal(object message, Exception exception)
        {
           _logger.Fatal($"Message: @{message}, Exception : @{exception}");
        }

        ///<inheritdoc/>
        public void FatalFormat(string format, params object[] args)
        {
            _logger.Fatal(format, args);
        }
    }
}