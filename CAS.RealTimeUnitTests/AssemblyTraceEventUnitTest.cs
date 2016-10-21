
using CAS.Lib.RTLib.Diagnostics;
using CAS.Lib.RTLib.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CAS.RealTime.UnitTests.Instrumentation;

namespace CAS.RealTime.UnitTests
{
  [TestClass]
  public class AssemblyTraceEventUnitTest
  {
    [TestMethod]
    public void AssemblyTraceEventTestMethod()
    {
      string _testPath = Path.GetTempPath();
      AdvancedDelimitedListTraceListener.ApplicationDataPath = _testPath;
      TraceSource _tracer = AssemblyTraceEvent.AssemblyTraceSource;
      Assert.IsNotNull(_tracer);
      Assert.AreEqual(1, _tracer.Listeners.Count);
      Dictionary<string, TraceListener> _listeners = _tracer.Listeners.Cast<TraceListener>().ToDictionary<TraceListener, string>(x => x.Name);
      Assert.IsTrue(_listeners.ContainsKey("LogFile"));
      TraceListener _listener = _listeners["LogFile"];
      Assert.IsNotNull(_listener);
      Assert.IsInstanceOfType(_listener, typeof(AdvancedDelimitedListTraceListener));
      AdvancedDelimitedListTraceListener _advancedListener = _listener as AdvancedDelimitedListTraceListener;
      Assert.IsNotNull(_advancedListener.Filter);
      Assert.IsInstanceOfType(_advancedListener.Filter, typeof(EventTypeFilter));
      EventTypeFilter _eventTypeFilter = _advancedListener.Filter as EventTypeFilter;
      Assert.AreEqual(SourceLevels.All, _eventTypeFilter.EventType);
      Assert.AreEqual<string>(Path.Combine(_testPath, @"log\CAS.RealTimeUnitTests.log"), _advancedListener.GetFileNAme());

    }
  }
}
