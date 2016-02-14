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
      
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CAS.Lib.RTLib.Tests
{
  /// <summary>
  ///This is a test class for TimerTest and is intended
  ///to contain all TimerTest Unit Tests
  ///</summary>
  [TestClass()]
  public class TimerTest
  {
    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }
    #region Additional test attributes
    // 
    //You can use the following additional attributes as you write your tests:
    //
    //Use ClassInitialize to run code before running the first test in the class
    //[ClassInitialize()]
    //public static void MyClassInitialize(TestContext testContext)
    //{
    //}
    //
    //Use ClassCleanup to run code after all tests in a class have run
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //Use TestInitialize to run code before running each test
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //}
    //
    //Use TestCleanup to run code after each test has run
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{
    //}
    //
    #endregion
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
        CAS.Lib.RTLib.Processes.Timer.WaitTimeout( myTimeout, mySW, monitor );
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
        CAS.Lib.RTLib.Processes.Timer.WaitTimeout( myTimeout, mySW );
        TimeSpan elapsed = mySW.Elapsed;
        Assert.IsTrue( elapsed >= myTimeout && elapsed < ( myTimeout + TimeSpan.FromMilliseconds( 20 ) ) );
        //mma.Add = Convert.ToInt64( elapsed.TotalMilliseconds );
      }
      //TestContext.WriteLine( mma.ToString() );
    }
  }
}