//<summary>
//  Title   : FacadeHandlerWaitTimeList - class prepared for test on HandlerWaitTimeList 
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace CAS.Lib.RTLibCom.Tests
{
  /// <summary>
  /// Summary description for UnitTest1
  /// </summary>
  [TestClass]
  public class HandlerWaitTimeListTest
  {
    public HandlerWaitTimeListTest()
    {
    }
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
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //
    #endregion
    /// <summary>
    /// This function is responsible for executing one test on the <see cref="HandlerWaitTimeList"/> (queue).
    /// </summary>
    /// <param name="CycleTimeInmSeconds">The cycle time in seconds.</param>
    /// <param name="Autoreset">if set to <c>true</c> auto-reset feature is enabled.</param>
    /// <param name="OperationTimeInMiliseconds">The time in ms of each operation on the list.</param>
    /// <param name="testTimefSeconds">The consistency should be checked after this number of seconds.</param>
    /// <param name="CheckConsistencySyncronously">if set to <c>true</c> check consistency synchronously.</param>
    private void OneTest(int CycleTimeInmSeconds, bool Autoreset, int OperationTimeInMiliseconds, int testTimefSeconds, double acceptableErrorInPercents)
    {
      TimeSpan _Cycle = new TimeSpan(0, 0, 0, 0, CycleTimeInmSeconds);
      System.Console.WriteLine("Test with {0} ms long procedure, Auto-reset:{1} ", OperationTimeInMiliseconds, Autoreset);
      using (FacadeHandlerWaitTimeList myList = new FacadeHandlerWaitTimeList(_Cycle, Autoreset, OperationTimeInMiliseconds))
      {
        WaitAndCheckConsistency(testTimefSeconds, myList, acceptableErrorInPercents);
      }
    }
    /// <summary>
    /// Waits the and check consistency.
    /// </summary>
    /// <param name="ConsistencyCheckAfterNumberOfSeconds">The consistency check after number of seconds.</param>
    /// <param name="myList">My list.</param>
    private void WaitAndCheckConsistency(int ConsistencyCheckAfterNumberOfSeconds, FacadeHandlerWaitTimeList myList, double maxError)
    {
      Thread.Sleep(TimeSpan.FromSeconds(ConsistencyCheckAfterNumberOfSeconds));
      Assert.IsTrue(myList.Consistency(maxError), "The list is not consistent)");
    }
    /// <summary>
    /// Test with auto-rest enabled and operation length 10 ms
    /// </summary>
    [TestMethod]
    public void GeneralTestAutoreset_10()
    {
      OneTest(2500, true, 10, 30, 1.4);
    }
    /// <summary>
    /// Test with [autorest] enabled and operation length 250 ms
    /// </summary>
    [TestMethod]
    public void GeneralTestAutoreset_250()
    {
      OneTest(2500, true, 250, 30, 4.5);
    }
    /// <summary>
    /// Test with [autorest] enabled and operation length 275 ms
    /// </summary>
    [TestMethod]
    public void GeneralTestAutoreset_275()
    {
      OneTest(2500, true, 275, 30, 3.3);
    }
    /// <summary>
    /// Test with [autorest] enabled and operation length 300 ms
    /// </summary>
    [TestMethod]
    public void GeneralTestAutoreset_300()
    {
      OneTest(2500, true, 300, 30, 0.9);
    }
    /// <summary>
    /// Test with [autorest] disabled and operation length 10 ms
    /// </summary>
    [TestMethod]
    public void GeneralTestNoAutoreset_10()
    {
      OneTest(2500, false, 10, 30, 1.4);
    }
    /// <summary>
    /// Test with [autorest] disabled and operation length 250 ms
    /// </summary>
    [TestMethod]
    public void GeneralTestNoAutoreset_250()
    {
      OneTest(2500, false, 250, 30, 6.5);
    }
    /// <summary>
    /// Test with [autorest] disabled and operation length 275 ms
    /// </summary>
    [TestMethod]
    public void GeneralTestNoAutoreset_275()
    {
      OneTest(2500, false, 275, 30, 3.3);
    }
    /// <summary>
    /// Test with auto rest disabled and operation length 300 ms
    /// </summary>
    [TestMethod]
    public void GeneralTestNoAutoreset_300()
    {
      OneTest(2500, false, 300, 30, 0.7);
    }
    /// <summary>
    /// Stresses test the WaitTime List with auto reset.
    /// </summary>
    [TestMethod]
    public void StressTestAutoreset()
    {
      OneTest(1000, true, 100, 60, 15);
    }
    /// <summary>
    /// Stresses test the WaitTime List with no auto reset.
    /// </summary>
    [TestMethod]
    public void StressTestNoAutoreset()
    {
      OneTest(1000, false, 100, 60, 15);
    }
  }
}
