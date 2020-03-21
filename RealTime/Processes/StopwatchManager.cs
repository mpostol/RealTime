//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.ProcessObserver.RealTime.Processes
{
  /// <summary>
  ///  Title   : Stopwatch mechanism implementation
  /// </summary>
  [System.Serializable]
  public class Stopwatch
  {
    #region private

    [System.NonSerialized]
    private System.Diagnostics.Stopwatch myStopwatch;

    /// <summary>
    /// Gets the curr delay.Reads the elapsed ticks from stopwatch
    /// </summary>
    /// <remarks>On a multiprocessor computer, it does not matter which processor the thread runs on.
    /// However, because of bugs in the BIOS or the Hardware Abstraction Layer (HAL), you can get
    /// different timing results on different processors. To specify processor affinity for a thread,
    /// use the ProcessThread.ProcessorAffinity method.</remarks>
    /// <value>The curr delay.</value>
    private ulong currDelay
    {
      get
      {
        long ret_value = myStopwatch.ElapsedTicks;
        if (ret_value < 0)
          ret_value = 0;
        return System.Convert.ToUInt64(ret_value);
      }
    }

    #endregion private

    #region public

    /// <summary>
    /// Gets the frequency of the timer as the number of ticks per second. This field is read-only.
    /// </summary>
    /// <remarks>The timer frequency indicates the timer precision and resolution. For example, a timer frequency of 2 million ticks per second equals a timer resolution of 500 nanoseconds per tick. In other words, because one second equals 1 billion nanoseconds, a timer frequency of 2 million ticks per second is equivalent to 2 million ticks per 1 billion nanoseconds, which can be further simplified to 1 tick per 500 nanoseconds.
    /// The Frequency value depends on the resolution of the underlying timing mechanism. If the installed hardware and operating system support a high-resolution performance counter, then the Frequency value reflects the frequency of that counter. Otherwise, the Frequency value is based on the system timer frequency.
    /// Because the Stopwatch frequency depends on the installed hardware and operating system, the Frequency value remains constant while the system is running.
    /// </remarks>
    /// <value>The SW frequency.</value>
    public static uint SWFrequency => (uint)System.Diagnostics.Stopwatch.Frequency;

    /// <summary>
    /// stops the stopwatch.
    /// </summary>
    /// <value>time measured by stopwatch</value>
    public ulong Stop
    {
      get
      {
        myStopwatch.Stop();
        return currDelay;
      }
    }

    /// <summary>
    /// starts the stopwatch
    /// </summary>
    /// <value>time measured by stopwatch</value>
    public ulong Start
    {
      get
      {
        ulong retV = currDelay;
        myStopwatch.Start();
        return retV;
      }
    }

    /// <summary>
    /// Reads the timer
    /// </summary>
    /// <value>time measured by stopwatch</value>
    public ulong Read => currDelay;

    /// <summary>
    /// Returns the value and resets the Stopwatch.
    /// </summary>
    /// <value>the period of time</value>
    public ulong Reset
    {
      get
      {
        ulong retV = currDelay;
        bool IsRunning = myStopwatch.IsRunning;
        myStopwatch.Reset();
        if (IsRunning)
          myStopwatch.Start();
        return retV;
      }
    }//Reset

    /// <summary>
    /// Reset and start the stopwatch.
    /// </summary>
    public void StartReset()
    {
      myStopwatch.Reset();
      myStopwatch.Start();
    }

    /// <summary>
    /// Converts the to s.
    /// </summary>
    /// <param name="val">value.</param>
    /// <returns></returns>
    public static uint ConvertTo_s(ulong val)
    {
      return (uint)(val / SWFrequency);
    }

    /// <summary>
    /// Converts the to ms.
    /// </summary>
    /// <param name="val">The value.</param>
    /// <returns></returns>
    public static uint ConvertTo_ms(ulong val)
    {
      ulong divider = SWFrequency / 1000;
      return (uint)(val / divider);
    }

    /// <summary>
    /// Converts the to us (micro seconds).
    /// </summary>
    /// <param name="val">The value.</param>
    /// <returns></returns>
    public static ulong ConvertTo_us(ulong val)
    {
      double mul = 1000000.0 / SWFrequency;
      return System.Convert.ToUInt64(val * mul);
    }

    /// <summary>
    /// Gets a value indicating whether this instance is running.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is running; otherwise, <c>false</c>.
    /// </value>
    public bool IsRunning => myStopwatch.IsRunning;

    /// <summary>
    /// Initializes a new instance of the <see cref="Stopwatch"/> class.
    /// </summary>
    public Stopwatch()
    {
      myStopwatch = new System.Diagnostics.Stopwatch();
    }

    #endregion public
  }//Stopwatch
}