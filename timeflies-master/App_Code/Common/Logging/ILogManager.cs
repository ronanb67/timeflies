using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TimeFlies.Common.Logging
{
  public interface ILogManager
  {
    void Critical(Exception ex);
    void Critical(Exception ex, string message);
    void Critical(Exception ex, string message, params object[] args);
    void Critical(string message);
    void Critical(string message, params object[] args);
    void Error(Exception ex);
    void Error(Exception ex, string message);
    void Error(Exception ex, string message, params object[] args);
    void Error(string message);
    void Error(string message, params object[] args);
    void Information(string message);
    void Information(string message, params object[] args);
    void Verbose(string message);
    void Verbose(string message, params object[] args);
    void Warning(string message);
    void Warning(string message, params object[] args);    

    void Write(string message, string category, TraceEventType eventType,
      IDictionary<string, Object> properties);
    void Write(string message, string category, TraceEventType eventType,
      IDictionary<string, Object> properties, Exception ex);
  }
}