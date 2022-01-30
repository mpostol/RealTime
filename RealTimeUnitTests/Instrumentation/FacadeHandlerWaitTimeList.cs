//__________________________________________________________________________________________________
//
//  Copyright (C) 2023, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using UAOOI.ProcessObserver.RealTime.Processes;
using UAOOI.ProcessObserver.RealTime.Utils;
using System;

namespace CAS.RealTime.UnitTests.Instrumentation
{
  internal class FacadeHandlerWaitTimeList : HandlerWaitTimeList<FacadeTODescriptor>
  {
    private static int m_FacadeHandlerWaitTimeListNumber = 0;
    private static object Mylock = new object();
    private System.Diagnostics.Stopwatch m_RunTime = new System.Diagnostics.Stopwatch();
    private MinMaxAvr m_OperationSpan = new MinMaxAvr(20);
    private MinMaxAvr m_CycleTime = new MinMaxAvr(20);
    private MinMaxAvr m_Error = new MinMaxAvr(20);
    private const int c_NumberOfItems = 10;
    private readonly TimeSpan m_OperationSpan_in_ms = TimeSpan.Zero;
    private readonly TimeSpan m_cycle = TimeSpan.Zero;
    private int m_Counter = 0;
    private readonly string m_Name = "";
    private const string c_NameTemplate = "FacadeHandlerWaitTimeList({0}, {1}, {2})";
    /// <summary>
    /// this function checks the results of the test (consistency of the list)
    /// </summary>
    public bool Consistency(double acceptableErrorInPercents)
    {
      DisableListManagerThread();
      double _result = Convert.ToDouble(Math.Abs(m_CycleTime.Max - m_CycleTime.Min)) / m_CycleTime.Avr * 100;
      System.Console.WriteLine
        (m_Name + "Consistency test: counter: {0}, run time elapsed: {1} ms error = {2:F3} %, max error {3}", m_Counter, m_RunTime.Elapsed.TotalMilliseconds, _result, acceptableErrorInPercents);
      return _result <= acceptableErrorInPercents;
    }
    /// <summary>
    /// Event handler invoked every time new values are available.
    /// </summary>
    /// <param name="min">The min.</param>
    /// <param name="max">The max.</param>
    /// <param name="average">The average.</param>
    public override void NewOvertimeCoefficient(long min, long max, long average)
    {
      Console.WriteLine(m_Name + "Queue priorities (Min/Avg/Max) ({0}/{1}/{2}), queue length= {3}", min, average, max, QueueLength);
    }
    /// <summary>
    /// Event handler invoked every time new values from minmaxavg_oper_time are available
    /// </summary>
    /// <param name="min">The min.</param>
    /// <param name="max">The max.</param>
    /// <param name="average">The average.</param>
    private void New_minmaxavg_oper_time_coeficients(long min, long max, long average)
    {
      Console.WriteLine(m_Name + "Operation time (Min/Avg/Max) ({0}/{1}/{2}) [ms], Number of executions = {3:000}", min, average, max, m_Counter);
    }
    /// <summary>
    /// Event handler invoked every time new values from minmaxavg_scan_rate are available
    /// </summary>
    /// <param name="min">The min.</param>
    /// <param name="max">The max.</param>
    /// <param name="average">The average.</param>
    private void New_minmaxavg_scan_rate_coeficients(long min, long max, long average)
    {
      Console.WriteLine(m_Name + "Scan rate (Min/Avg/Max) ({0}/{1}/{2}) [ms], Number of executions = {3:000}", min, average, max, m_Counter);
    }
    /// <summary>
    /// Event handler invoked every time new values from minmaxavg_error are available
    /// </summary>
    /// <param name="min">The min.</param>
    /// <param name="max">The max.</param>
    /// <param name="avr">The avr.</param>
    private void MinmaxavgErrorMarkNewVal(long min, long max, long avr)
    {
      Console.WriteLine(m_Name + "Error (Min/Avg/Max) ({0}/{1}/{2}) [%*100], Number of executions = {3:000}", min, avr, max, m_Counter);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="FacadeHandlerWaitTimeList"/> class responsible for test on wait time list.
    /// </summary>
    /// <param name="cycle">The cycle of each task (element) on the list.</param>
    /// <param name="autoReset">if set to <c>true</c> auto-reset] feature is enabled.</param>
    /// <param name="OperationLong_in_ms">How long is the operation in ms?</param>
    public FacadeHandlerWaitTimeList(TimeSpan cycle, bool autoReset, int operationSpan)
      : base(autoReset, String.Format("FacadeHandlerWaitTimeList({0}, {1}, {2})", operationSpan, autoReset, m_FacadeHandlerWaitTimeListNumber))
    {
      lock (Mylock)
      {
        m_Name = String.Format(c_NameTemplate, operationSpan, autoReset, m_FacadeHandlerWaitTimeListNumber++);
        m_cycle = cycle;
        Console.WriteLine("WaitTimeList: cycle {0}, number of items: {1}", m_cycle, c_NumberOfItems);
        m_Counter = 0;
        this.m_OperationSpan.MarkNewVal += new MinMaxAvr.newVal(New_minmaxavg_oper_time_coeficients);
        this.m_CycleTime.MarkNewVal += new MinMaxAvr.newVal(New_minmaxavg_scan_rate_coeficients);
        this.m_Error.MarkNewVal += new MinMaxAvr.newVal(MinmaxavgErrorMarkNewVal);
        this.m_OperationSpan_in_ms = new TimeSpan(0, 0, 0, 0, operationSpan);
        for (int i = 0; i < c_NumberOfItems; i++)
          (new FacadeTODescriptor(autoReset, this, m_cycle, m_OperationSpan_in_ms)).ResetCounter();
        m_RunTime.Start();
      }
    }
    #region HandlerWaitTimeList implementation
    protected override void Handler(FacadeTODescriptor myDsc)
    {
      m_Counter++;
      myDsc.DoTest(ref m_Error, ref m_OperationSpan, ref m_CycleTime);
    }
    #endregion
  }
  /// <summary>
  /// implementation of WaitTimeList Timeout descriptor
  /// </summary>
  internal class FacadeTODescriptor : WaitTimeList<FacadeTODescriptor>.TODescriptor
  {
    private readonly TimeSpan m_OperationDuration;
    private readonly bool m_AutoReset;
    private TimeSpan m_CycleSpan;
    private TimeSpan m_ExpectedTime;
    private TimeSpan m_CurrentTime;
    private System.Diagnostics.Stopwatch m_RunTimeStopwatch = new System.Diagnostics.Stopwatch();
    private System.Diagnostics.Stopwatch m_OperationSpanStopwatch = new System.Diagnostics.Stopwatch();
    private System.Diagnostics.Stopwatch m_CycleTimeStopwatch = new System.Diagnostics.Stopwatch();
    private readonly WaitTimeList<FacadeTODescriptor> m_parent;
    /// <summary>
    /// Does the test - this is main function that is executed as the handler.
    /// </summary>
    /// <param name="error">The _minmaxavg_error counter</param>
    /// <param name="OperationSpan">The _minmaxavg_opertime counter.</param>
    /// <param name="cycleTime">The _minmaxavg_scanrate counter.</param>
    /// <returns></returns>
    public void DoTest(ref MinMaxAvr error, ref MinMaxAvr operationSpan, ref MinMaxAvr cycleTime)
    {
      m_OperationSpanStopwatch.Reset();
      m_OperationSpanStopwatch.Start();
      m_CycleTimeStopwatch.Stop();
      cycleTime.Add = m_CycleTimeStopwatch.ElapsedMilliseconds;
      m_CycleTimeStopwatch.Reset();
      m_CycleTimeStopwatch.Start();
      m_CurrentTime = m_RunTimeStopwatch.Elapsed;
      double _currentError;
      if (m_AutoReset)
      {
        _currentError = (Math.Abs(m_CurrentTime.TotalMilliseconds - m_ExpectedTime.TotalMilliseconds) / m_CycleSpan.TotalMilliseconds) * 100.0;
        m_ExpectedTime = m_RunTimeStopwatch.Elapsed + m_CycleSpan;
        System.Threading.Thread.Sleep(Convert.ToInt32(m_OperationDuration.TotalMilliseconds));
      }
      else
      {
        _currentError = (Math.Abs(m_CurrentTime.TotalMilliseconds - m_ExpectedTime.TotalMilliseconds) / (m_CycleSpan + m_OperationDuration).TotalMilliseconds) * 100.0;
        System.Threading.Thread.Sleep(Convert.ToInt32(m_OperationDuration.TotalMilliseconds));
        this.ResetCounter();
        m_ExpectedTime = m_RunTimeStopwatch.Elapsed + m_CycleSpan;
      }
      error.Add = (long)_currentError;
      m_OperationSpanStopwatch.Stop();
      operationSpan.Add = m_OperationSpanStopwatch.ElapsedMilliseconds;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="FacadeTODescriptor"/> class.
    /// </summary>
    /// <param name="AutoReset">if set to <c>true</c> auto reset feature is enabled.</param>
    /// <param name="parentList">The parent list.</param>
    /// <param name="cycle">The cycle.</param>
    /// <param name="operationDuration">Time span needed to execute the operation?</param>
    public FacadeTODescriptor(bool AutoReset, WaitTimeList<FacadeTODescriptor> parentList, TimeSpan cycle, TimeSpan operationDuration)
      : base(parentList, cycle)
    {
      m_parent = parentList;
      m_CycleSpan = cycle;
      m_OperationDuration = operationDuration;
      m_AutoReset = AutoReset;
      m_RunTimeStopwatch.Reset();
      m_RunTimeStopwatch.Start();
      m_CycleTimeStopwatch.Reset();
      m_CycleTimeStopwatch.Start();
    }
  }
}
