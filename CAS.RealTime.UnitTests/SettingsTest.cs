using Microsoft.VisualStudio.TestTools.UnitTesting;
//<summary>
//  Title   : RTLib.Tests.Settings
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    2007- mpostol - created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2014, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using TestAssert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CAS.Lib.RTLib.Tests
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
      TestAssert.AreEqual("CAS.Lib.RTLib", CAS.Lib.RTLib.Properties.Settings.Default.TraceName, "Trace name must be well defined and cannot be changed - it is breaking feature");
    }
  }
}
