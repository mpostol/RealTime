//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using UAOOI.ProcessObserver.RealTime.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CAS.RealTime.UnitTests
{
  [TestClass]
  public class MinMaxAvrUnitTest
  {
    [TestMethod]
    public void MinMaxAvrTestMethod()
    {
      MinMaxAvr _testVar = new MinMaxAvr(10);
      Assert.AreEqual(_testVar.Max, 0);
      Assert.AreEqual(_testVar.Min, 0);
      Assert.AreEqual(_testVar.Avr, 0);
      bool _result = false;
      _testVar.MarkNewVal += (x, z, y) => _result = (x == 10) && (y == 14) && (z == 19);
      for (int i = 10; i <= 20; i++)
        _testVar.Add = i;
      Assert.IsTrue(_result, "MinMaxAvr doesn't work as expected");
      Assert.AreEqual(_testVar.Max, 19);
      Assert.AreEqual(_testVar.Min, 10);
      Assert.AreEqual(_testVar.Avr, 14);
    }
  }
}
