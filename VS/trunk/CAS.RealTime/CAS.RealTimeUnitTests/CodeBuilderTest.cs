//<summary>
//  Title   : Tests for CodeBuilder
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  20080503: mzbrzezny: created
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using CAS.Lib.RTLib.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CAS.RealTime.UnitTests
{
  /// <summary>
  ///This is a test class for CodeBuilderTest and is intended
  ///to contain all CodeBuilderTest Unit Tests
  ///</summary>
  [TestClass()]
  public class CodeBuilderTest
  {
    /// <summary>
    ///A test for RunCode and simple math calculation
    ///</summary>
    [TestMethod()]
    public void RunCodeTest_simplemath()
    {
      for (int i = 1; i < 100; i++)
      {
        string expression = String.Format("({0}+{0}/{0}-{0}*{0})", i);
        double result = i + i / i - i * i;
        CodeBuilder cb = new CodeBuilder(expression);
        cb.RunCode();
        Assert.AreEqual(result.ToString(), cb.OutputTextFromLastRun, "Problem in test:"
          + i.ToString() + "\r\nSourceCode:\r\n" + cb.SourceCode);
      }
    }
    /// <summary>
    ///A test for RunCode and some code inside (e.g. for loop)
    ///</summary>
    [TestMethod()]
    public void RunCodeTest_somecode()
    {
      for (int i = 1; i < 100; i++)
      {
        string expression = String.Format("answer; answer = 0; for (int j = {0}; j < 1000; j += {0}) answer += j;", i);
        int answer = 0; for (int j = i; j < 1000; j += i) answer += j;
        CodeBuilder cb = new CodeBuilder(expression);
        cb.RunCode();
        Assert.AreEqual(answer.ToString(), cb.OutputTextFromLastRun, "Problem in test:"
          + i.ToString() + "\r\nSourceCode:\r\n" + cb.SourceCode);
      }
    }
    /// <summary>
    ///A test for RunCode and some code inside (e.g. for loop)
    ///</summary>
    [TestMethod()]
    public void RunCodeTest_time_functions()
    {
      string expression = "System.DateTime.Now.Second";
      CodeBuilder cb = new CodeBuilder(expression);
      for (int i = 1; i < 10; i++)
      {
        int answer = System.DateTime.Now.Second;
        cb.RunCode();
        Assert.AreEqual(answer.ToString(), cb.OutputTextFromLastRun, "Problem in test:"
          + i.ToString() + "\r\nSourceCode:\r\n" + cb.SourceCode);
        System.Threading.Thread.Sleep(500);
      }
    }

  }
}
