//<summary>
//  Title   : Timer Test
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2009, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using CAS.Lib.RTLib.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CAS.RealTime.UnitTests
{
  /// <summary>
  ///This is a test class for TimerTest and is intended to contain all TimerTest Unit Tests
  ///</summary>
  [TestClass()]
  public class TimerTest
  {
    /// <summary>
    ///A test for WaitTimeout
    ///</summary>
    [TestMethod()]
    public void WaitTimeoutTest1()
    {
      TimeSpan myTimeout = new TimeSpan( 0, 0, 1 );
      System.Diagnostics.Stopwatch mySW = new System.Diagnostics.Stopwatch();
      mySW.Start();
      object monitor = new object();
      lock ( monitor )
        Timer.WaitTimeout( myTimeout, mySW, monitor );
      Assert.IsTrue( mySW.Elapsed > myTimeout );
    }
    /// <summary>
    ///A test for WaitTimeout
    ///</summary>
    [TestMethod()]
    public void WaitTimeoutTest()
    {
      //MinMaxAvr mma = new MinMaxAvr( 10 );
      for ( int i = 0; i < 10; i++ )
      {
        TimeSpan myTimeout = TimeSpan.FromSeconds( 1 );
        System.Diagnostics.Stopwatch mySW = new System.Diagnostics.Stopwatch();
        mySW.Start();
        Timer.WaitTimeout( myTimeout, mySW );
        TimeSpan elapsed = mySW.Elapsed;
        Assert.IsTrue( elapsed >= myTimeout && elapsed < ( myTimeout + TimeSpan.FromMilliseconds( 40 ) ), $"At {i} elapsed = {elapsed}" );
        //mma.Add = Convert.ToInt64( elapsed.TotalMilliseconds );
      }
    }
  }
}