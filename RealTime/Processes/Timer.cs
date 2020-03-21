//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Threading;

namespace UAOOI.ProcessObserver.RealTime.Processes
{
  /// <summary>
  /// Implementation of time relationship utilities.
  /// </summary>
  public sealed class Timer
  {
    /// <summary>
    /// Process enters monitor and waits
    /// </summary>
    /// <param name="miliseconds">Number of ticks to wait</param>
    /// <param name="monitor">Class which is a monitor protecting resources</param>
    public static void Wait(uint miliseconds, object monitor)
    {
      Monitor.Exit(monitor);
      Thread.Sleep((int)miliseconds);
      Monitor.Enter(monitor);
    }

    /// <summary>
    /// Waits the timeout.
    /// </summary>
    /// <param name="timeout">timeout to wait <see cref="TimeSpan"/></param>
    /// <param name="stopwatch">Stopwatch ( <see cref="System.Diagnostics.Stopwatch"/>)that is used for time measurement.</param>
    public static void WaitTimeout(TimeSpan timeout, System.Diagnostics.Stopwatch stopwatch)
    {
      TimeSpan minWaitTime = TimeSpan.FromMilliseconds(20);
      System.Diagnostics.Debug.Assert(stopwatch.IsRunning, "StopWatch is not running");
      while (stopwatch.Elapsed < timeout)
        if (stopwatch.Elapsed + minWaitTime < timeout)
          Thread.Sleep(timeout - stopwatch.Elapsed);
    }

    /// <summary>
    /// Process enters monitor and waits the timeout.
    /// </summary>
    /// <param name="timeout">timeout to wait <see cref="TimeSpan"/></param>
    /// <param name="stopwatch">Stopwatch ( <see cref="System.Diagnostics.Stopwatch"/>)that is used for time messaurement.</param>
    /// <param name="monitor">The monitor. Class witch is a monitor protecting recurses</param>
    public static void WaitTimeout(TimeSpan timeout, System.Diagnostics.Stopwatch stopwatch, object monitor)
    {
      Monitor.Exit(monitor);
      WaitTimeout(timeout, stopwatch);
      Monitor.Enter(monitor);
    }

    /// <summary>
    /// Gets the number of milliseconds (old name ticks) in one second.
    /// </summary>
    /// <value>the number of milliseconds (old name ticks) in one second.</value>
    [Obsolete("This is a deprecated property. This value is 1000, it is obvious that second contains 1000 milliseconds")]
    public static uint TInOneSecond => 1000;

    /// <summary>
    /// Returns the larger of two <see cref="TimeSpan"/>time spans.
    /// </summary>
    /// <param name="val1">The first of two <see cref="TimeSpan"/>time spans to compare.</param>
    /// <param name="val2">The second of two <see cref="TimeSpan"/>time spans to compare.</param>
    /// <returns>The larger <see cref="TimeSpan"/></returns>
    public static TimeSpan Max(TimeSpan val1, TimeSpan val2)
    {
      return val1 > val2 ? val1 : val2;
    }

    /// <summary>
    /// Returns the smaller of two <see cref="TimeSpan"/>time spans to compare.
    /// </summary>
    /// <param name="val1">The first of two <see cref="TimeSpan"/>time spans to compare.</param>
    /// <param name="val2">The second of two <see cref="TimeSpan"/>time spans to compare.</param>
    /// <returns>The smaller <see cref="TimeSpan"/></returns>
    public static TimeSpan Min(TimeSpan val1, TimeSpan val2)
    {
      return val1 < val2 ? val1 : val2;
    }

    /// <summary>
    /// Returns a <see cref="System.TimeSpan"/> that represents a specified number of microseconds.
    /// </summary>
    /// <param name="microseconds">A number of microseconds.</param>
    /// <returns>A <see cref="System.TimeSpan"/> that represents microseconds.</returns>
    public static TimeSpan FromUSeconds(double microseconds)
    {
      return TimeSpan.FromMilliseconds(microseconds / 1000);
    }

    /// <summary> Returns a <see cref="uint"/> of the time <see cref="System.TimeSpan"/> structure expressed in whole
    /// microseconds.
    /// </summary>
    /// <param name="time">A <see cref="System.TimeSpan"/> object representing the time.</param>
    /// <returns>The total number of microseconds represented by the time.</returns>
    public static uint ToUSeconds(TimeSpan time)
    {
      return Convert.ToUInt32(time.TotalMilliseconds * 1000);
    }
  }
}