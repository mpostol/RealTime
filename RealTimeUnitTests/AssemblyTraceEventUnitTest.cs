using UAOOI.ProcessObserver.RealTime.Processes;
using CAS.RealTime.UnitTests.Instrumentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CAS.RealTime.UnitTests
{
  [TestClass]
  public class AssemblyTraceEventUnitTest
  {
    [TestMethod]
    public void AssemblyTraceEventTestMethod()
    {
      TraceSource _tracer = AssemblyTraceEvent.AssemblyTraceSource;
      Assert.IsNotNull(_tracer);
      Assert.AreEqual(1, _tracer.Listeners.Count);
      Dictionary<string, TraceListener> _listeners = _tracer.Listeners.Cast<TraceListener>().ToDictionary<TraceListener, string>(x => x.Name);
      Assert.IsTrue(_listeners.ContainsKey("LogFile"));
      TraceListener _listener = _listeners["LogFile"];
      Assert.IsNotNull(_listener);
      Assert.IsInstanceOfType(_listener, typeof(DelimitedListTraceListener));
      DelimitedListTraceListener _advancedListener = _listener as DelimitedListTraceListener;
      Assert.IsNotNull(_advancedListener.Filter);
      Assert.IsInstanceOfType(_advancedListener.Filter, typeof(EventTypeFilter));
      EventTypeFilter _eventTypeFilter = _advancedListener.Filter as EventTypeFilter;
      Assert.AreEqual(SourceLevels.All, _eventTypeFilter.EventType);
      string _testPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      Assert.AreEqual<string>(Path.Combine(_testPath, @"log\CAS.RealTimeUnitTests.log"), _advancedListener.GetFileName());
    }
    [TestMethod]
    public void LogFileExistsTest()
    {
      TraceSource _tracer = AssemblyTraceEvent.AssemblyTraceSource;
      TraceListener _listener = _tracer.Listeners.Cast<TraceListener>().Where<TraceListener>(x => x.Name == "LogFile").First<TraceListener>();
      Assert.IsNotNull(_listener);
      DelimitedListTraceListener _advancedListener = _listener as DelimitedListTraceListener;
      Assert.IsNotNull(_advancedListener);
      Assert.IsFalse(String.IsNullOrEmpty(_advancedListener.GetFileName()));
      FileInfo _logFileInfo = new FileInfo(_advancedListener.GetFileName());
      long _startLength = _logFileInfo.Length;
      _tracer.TraceEvent(TraceEventType.Information, 0, "LogFileExistsTest is executed");
      Assert.IsFalse(String.IsNullOrEmpty(_advancedListener.GetFileName()));
      _logFileInfo.Refresh();
      Assert.IsTrue(_logFileInfo.Exists);
      Assert.IsTrue(_logFileInfo.Length > _startLength);
    }
  }
}
