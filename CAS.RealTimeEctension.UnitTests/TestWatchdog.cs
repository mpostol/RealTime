//<summary>
//  Title   : TestWatchdog
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    2007 mpostol: created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http://www.cas.eu
//</summary>

using CAS.RealTime.UnitTests.Instrumentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CAS.RealTime.UnitTests
{
  /// <summary>
  /// class that is used to test the Watchdog (this is unit test class)
  /// </summary>

  [TestClass]
  public class TestWatchdog
  {
    /// <summary>
    /// Tests the reset.
    /// </summary>
    [TestMethod]
    public void TestReset()
    {
      FacadeWatchDog wd = new FacadeWatchDog( "testFacadeWatchDog", 123456 );
      NotContexClass nc = new NotContexClass();
      long cycles = 0;
      System.Diagnostics.Stopwatch m_Stopwatch = new System.Diagnostics.Stopwatch();
      Console.WriteLine( "Stress test of a context for 40 sec." );
      TimeSpan dedline = new TimeSpan( 0, 0, 40 );
      m_Stopwatch.Start();
      while ( m_Stopwatch.Elapsed < dedline )
      {
        cycles++;
        wd.ShortMethod("ShortMethod");
      }
      Console.WriteLine( "Time of one cycle = {0} mS", dedline.TotalMilliseconds / cycles );
      Console.WriteLine( "Stress test of clear class for 40 sec." );
      m_Stopwatch.Reset();
      cycles = 0;
      m_Stopwatch.Start();
      while ( m_Stopwatch.Elapsed < dedline )
      {
        cycles++;
        nc.ShortMethod();
      }
      Console.WriteLine( "Time of one cycle = {0} mS", dedline.TotalMilliseconds / cycles );
      wd.LongMethod("LongMethod", 7654321);
      //NUnit.Framework.Assert.AreEqual( 1, WatchdogProperty.count, "There should be one restart noted" );
    }
  }
}
