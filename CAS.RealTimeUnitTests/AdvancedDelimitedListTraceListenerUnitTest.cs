
using CAS.Lib.CodeProtect;
using CAS.Lib.RTLib.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CAS.RealTime.UnitTests.Instrumentation;

namespace CAS.RealTime.UnitTests
{
  [TestClass]
  public class AdvancedDelimitedListTraceListenerUnitTest
  {

    [TestMethod]
    public void ConstructorTestMethod()
    {
      string _testFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TesFileName.log");
      using (AdvancedDelimitedListTraceListener _Listener = new AdvancedDelimitedListTraceListener(_testFileName))
      {
        Assert.AreEqual(";", _Listener.Delimiter);
        Assert.IsNull(_Listener.Filter);
        Assert.IsTrue(String.IsNullOrEmpty(_Listener.Name));
        Assert.AreEqual(TraceOptions.None, _Listener.TraceOutputOptions);
        Assert.IsNotNull(_Listener.Writer);
        Assert.AreEqual<string>(_testFileName, _Listener.GetFileNAme());
      }
    }
    [TestMethod]
    public void ApplicationDataPathTestMethod1()
    {
      AdvancedDelimitedListTraceListener.ApplicationDataPath = Path.Combine(InstallContextNames.ApplicationDataPath, Guid.NewGuid().ToString());
      string _testFileName = @"|ApplicationDataPath|TesFileName.log";
      using (AdvancedDelimitedListTraceListener _Listener = new AdvancedDelimitedListTraceListener(_testFileName))
        Assert.AreEqual<string>(Path.Combine(AdvancedDelimitedListTraceListener.ApplicationDataPath, "TesFileName.log"), _Listener.GetFileNAme());
    }
    [TestMethod]
    public void SpecialFolderPathTestMethod1()
    {
      AdvancedDelimitedListTraceListener.ApplicationDataPath = InstallContextNames.ApplicationDataPath;
      Environment.SpecialFolder _testFolder = Environment.SpecialFolder.ApplicationData;
      string _testFileName = $@"|{_testFolder}|TesFileName.log";
      using (AdvancedDelimitedListTraceListener _Listener = new AdvancedDelimitedListTraceListener(_testFileName))
        Assert.AreEqual<string>(Path.Combine(Environment.GetFolderPath(_testFolder), "TesFileName.log"), _Listener.GetFileNAme());
    }
    [TestMethod]
    public void ConfigTraceSourceTest()
    {
      TraceSource _tracer = new TraceSource("CAS.RealTimeUnitTests.TraceSource");
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
      //Writer
      using (TestTextWriter _writer = new TestTextWriter())
      {
        _advancedListener.Writer = _writer;
        _tracer.TraceEvent(TraceEventType.Error, 0, "Test message");
        Assert.IsTrue(_writer.WriteCount > 5);
        int _writeCount = _writer.WriteCount;
        _advancedListener.Flush();
        Assert.AreEqual<int>(_writeCount, _writer.WriteCount);
        _advancedListener.Close();
        Assert.AreEqual<int>(_writeCount, _writer.WriteCount);
      }

      //Test Switch
      Assert.IsNotNull(_tracer.Switch);
      Assert.AreEqual<string>("CAS.RealTimeUnitTests.Switch", _tracer.Switch.DisplayName);
      Assert.AreEqual<SourceLevels>(SourceLevels.All, _tracer.Switch.Level);

      //Trace
      Assert.IsFalse(Trace.Listeners.Cast<TraceListener>().Where<TraceListener>(x => x.Name == "LogFile").Any<TraceListener>());
    }

    #region instrumantation
    private class TestTextWriter : TextWriter
    {
      internal int WriteCount = 0;
      public override Encoding Encoding
      {
        get
        {
          return Encoding.ASCII;
        }
      }
      public override void Write(string value)
      {
        WriteCount++;
      }
    }

    #endregion

  }
}
