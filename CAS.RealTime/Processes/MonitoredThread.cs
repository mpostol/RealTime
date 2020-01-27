//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace CAS.Lib.RTLib.Processes
{
  using System;
  using System.Collections;
  using System.Diagnostics;
  using System.Threading;

  /// <summary>
  ///  Implements the watch dog mechanism for Thread's. 
  /// </summary>
  public class MonitoredThread
  {
    
    #region PRIVATE STATIC
    private static ArrayList threadsList = new ArrayList();
    private static void Handler()
    {
      while (true)
      {
        Thread.Sleep(TimeSpan.FromSeconds(1));
        lock (threadsList)
          foreach (MonitoredThread thr in threadsList)
            thr.decCounter();
      }
    }
    static MonitoredThread()
    {
      Manager.StartProcess(new ThreadStart(Handler), "MonitoredThreadHndl", true, ThreadPriority.Highest);
    }
    #endregion

    #region PRIVATE
    private readonly string Name;
    private int internalCounter;
    private bool on;
    private EventLogMonitor myEventLog;
    private ushort myTimeOut;
    private void decCounter()
    {
      lock (this)
      {
        if (!on)
          return;
        internalCounter--;
        if (internalCounter == 0)
        {
          string _exceptionMessage = myEventLog.GetMessage + " process:" + Name;
          myEventLog.SetMessage = _exceptionMessage;
          myEventLog.WriteEntry();
          throw new ApplicationException(_exceptionMessage);
        }
      }
    }
    #endregion

    #region PUBLIC
    /// <summary>
    /// Resets the watch dog.
    /// </summary>
    /// <param name="category">The category.</param>
    public void ResetWatchDog(short category)
    {
      lock (this)
      {
        internalCounter = myTimeOut;
        myEventLog.SetCategory = category;
      }
    }
    /// <summary>
    /// Resets the watch dog.
    /// </summary>
    /// <param name="category">The category.</param>
    /// <param name="timeout">The timeout.</param>
    public void ResetWatchDog(short category, ushort timeout)
    {
      lock (this)
      {
        on = (timeout > 0);
        myTimeOut = timeout;
        internalCounter = timeout;
        myEventLog.SetCategory = category;
      }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="MonitoredThread"/> class.
    /// </summary>
    /// <param name="timeout">The timeout.</param>
    /// <param name="message">The message.</param>
    /// <param name="eventID">The event ID.</param>
    /// <param name="process">The process.</param>
    /// <param name="processName">Name of the process.</param>
    /// <param name="isBackground">if set to <c>true</c> [is background].</param>
    /// <param name="priority">The priority.</param>
    public MonitoredThread(ushort timeout, string message, int eventID, ThreadStart process, string processName, bool isBackground, ThreadPriority priority)
    {
      myEventLog = new EventLogMonitor("Time out error: " + message, EventLogEntryType.Error, 2000 + eventID, 0);
      ResetWatchDog(short.MaxValue, timeout);
      Name = processName;
      lock (threadsList)
        threadsList.Add(this);
      Manager.StartProcess(process, processName, isBackground, priority);
    }
    #endregion

  }
}
