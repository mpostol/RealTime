using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAS.Lib.RTLib.Utils;

namespace CAS.Lib.RTLibCom.Tests
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
      _testVar.markNewVal += (x, z, y) => _result = (x == 10) && (y == 14) && (z == 19);
      for (int i = 10; i <= 20; i++)
        _testVar.Add = i;
      Assert.IsTrue(_result, "MinMaxAvr doesn't work as expected");
      Assert.AreEqual(_testVar.Max, 19);
      Assert.AreEqual(_testVar.Min, 10);
      Assert.AreEqual(_testVar.Avr, 14);
    }
  }
}
