//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CAS.Lib.RTLib.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CAS.RealTime.UnitTests
{
  /// <summary>
  /// Test Settings unit tests.
  /// </summary>
  [TestClass]
  public class TestSettings
  {
    /// <summary>
    /// SettingsTestFixture test
    /// </summary>
    [TestMethod]
    public void SettingsTestFixture()
    {
      string _traceName = string.Empty;
      AssemblyTraceEvent.GerTraceName(x => _traceName = x);
     Assert.AreEqual("CAS.RealTime", _traceName, "Trace name must be well defined and cannot be changed - it is breaking feature");
    }
  }
}
