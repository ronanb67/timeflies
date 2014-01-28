using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeFlies.Common.Logging;

namespace TimeFliesBy.WebUI
{
  public class LogManager : ILogManager
  {
    public void Critical(Exception ex)
    {
      EmailService.ErrorEmail(ex);
    }

    public void Critical(Exception ex, string message)
    {
      EmailService.ErrorEmail(ex, message);
    }

    public void Critical(Exception ex, string message, params object[] args)
    {
      EmailService.ErrorEmail(ex, String.Format(message, args));
    }

    public void Critical(string message)
    {
      EmailService.ErrorEmail(message);
    }

    public void Critical(string message, params object[] args)
    {
      EmailService.ErrorEmail(String.Format(message, args));
    }

    public void Error(Exception ex)
    {
      EmailService.ErrorEmail(ex);
    }

    public void Error(Exception ex, string message)
    {
      EmailService.ErrorEmail(ex, message);
    }

    public void Error(Exception ex, string message, params object[] args)
    {
      EmailService.ErrorEmail(ex, String.Format(message, args));
    }

    public void Error(string message)
    {
      EmailService.ErrorEmail(message);
    }

    public void Error(string message, params object[] args)
    {
      EmailService.ErrorEmail(String.Format(message, args));
    }

    public void Information(string message)
    {
      
    }

    public void Information(string message, params object[] args)
    {
      
    }

    public void Verbose(string message)
    {
      
    }

    public void Verbose(string message, params object[] args)
    {
      
    }

    public void Warning(string message)
    {
      
    }

    public void Warning(string message, params object[] args)
    {
      
    }

    public void Write(string message, string category, System.Diagnostics.TraceEventType eventType, IDictionary<string, object> properties)
    {
      
    }

    public void Write(string message, string category, System.Diagnostics.TraceEventType eventType, IDictionary<string, object> properties, Exception ex)
    {
      
    }
  }
}