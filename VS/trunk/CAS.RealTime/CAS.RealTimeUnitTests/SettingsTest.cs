
using CAS.Lib.RTLib.Properties;
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
      Assert.AreEqual("CAS.RealTime", Settings.Default.TraceName, "Trace name must be well defined and cannot be changed - it is breaking feature");
    }
  }
}
