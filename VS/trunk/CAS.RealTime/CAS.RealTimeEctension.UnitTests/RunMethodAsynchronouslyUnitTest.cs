//<summary>
//  Title   : RunMethodAsynchronouslyUnitTest
//  System  : Microsoft VisulaStudio 2013 / C#
//  $LastChangedDate:$
//  $Rev:$
//  $LastChangedBy:$
//  $URL:$
//  $Id:$
//
//  Copyright (C) 2014, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>
      
using CAS.Lib.RTLib.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace CAS.Lib.RTLibCom.Tests
{
  [TestClass]
  public class RunMethodAsynchronouslyUnitTest
  {
    private Condition m_MEthodFinished = new Condition();
    [TestMethod]
    public void RunMethodAsynchronouslyTestMethod()
    {
      RunMethodAsynchronously runasync = new RunMethodAsynchronously(delegate(object[] param) { WaitAndCheckConsistency((int)param[0], (int)param[1]); });
      runasync.RunAsync(new object[] { 5, 5 });
      bool _res = false;
      lock (this)
        _res = m_MEthodFinished.Wait(this, 1200);
      Assert.IsTrue(_res, "Calling asynchronous method filed.");
    }
    [TestMethod]
    public void RunMethodAsynchronouslyTimeOutTestMethod()
    {
      RunMethodAsynchronously runasync = new RunMethodAsynchronously(delegate(object[] param) { WaitAndCheckConsistency((int)param[0], (int)param[1]); });
      runasync.RunAsync(new object[] { 5, 5 });
      bool _res = false;
      lock (this)
        _res = m_MEthodFinished.Wait(this, 500);
      Assert.IsFalse(_res, "Calling asynchronous method filed.");
    }
    private void WaitAndCheckConsistency(int par1, int par2)
    {
      Thread.Sleep(1000);
      m_MEthodFinished.Notify();
    }
  }
}
